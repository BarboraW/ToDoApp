using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAppServer.Controllers;
using ToDoAppServer.Dtos;
using ToDoAppServer.Extensions;
using ToDoAppServer.Models;
using ToDoAppServer.Repositories;
using Xunit;

namespace ToDoAppServer.Tests
{
    public class TasksControllerTests
    {
        [Fact]
        public void GetTasks_ReturnsCollectionOfTasks()
        {
            //Arrange
            var repository = new InMemoryRepository();

            TaskObj task1 = new()
            {
                ID = new Guid(),
                Name = "Test",
                Priority = 1,
                Status = Status.NotStarted,
                CreatedDate = DateTime.Now,
            };

            TaskObj task2 = new()
            {
                ID = new Guid(),
                Name = "Test2",
                Priority = 2,
                Status = Status.Completed,
                CreatedDate = DateTime.Now,
            };

            repository.CreateTask(task1);
            repository.CreateTask(task2);

            var controller = new TasksController(repository);

            //Act
            var result = controller.GetTasks();

            //Assert
            Assert.Collection(result,
                 task => Assert.Equal(task1.AsDto(), task),
                 task => Assert.Equal(task2.AsDto(), task)
            );
        }

        [Fact]
        public void GetTasks_ReturnsEmptyList_WhenNoTasksExist()
        {
            // Arrange
            var repository = new InMemoryRepository();
            var controller = new TasksController(repository);

            // Act
            var result = controller.GetTasks();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetTaskById_ValidId_ReturnsTaskObjDto()
        {
            // Arrange
            var repository = new InMemoryRepository();
            var controller = new TasksController(repository);

            var validId = Guid.NewGuid();

            TaskObj task = new()
            {
                ID = validId,
                Name = "Test",
                Priority = 1,
                Status = Status.NotStarted,
                CreatedDate = DateTime.Now,
            };

            repository.CreateTask(task);

            //Act
            var result = controller.GetTaskById(validId);
            var taskObjDto = result.Value;

            //Assert
            Assert.IsType<ActionResult<TaskObjDto>>(result);
            Assert.Equal(task.ID, taskObjDto.ID);
            Assert.Equal(task.Name, taskObjDto.Name);
            Assert.Equal(task.Priority, taskObjDto.Priority);
            Assert.Equal(task.Status, taskObjDto.Status);
        }

        [Fact]
        public void GetTaskById_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var repository = new InMemoryRepository();
            var controller = new TasksController(repository);

            var validId = Guid.NewGuid();
            var invalidId = Guid.NewGuid();

            TaskObj task = new()
            {
                ID = validId,
                Name = "Test",
                Priority = 1,
                Status = Status.NotStarted,
                CreatedDate = DateTime.Now,
            };

            //Act
            var result = controller.GetTaskById(invalidId);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetTaskById_UnexpectedError_ReturnsInternalServerError()
        {
            // Arrange
            var mockRepository = new Mock<IRepository>();
            var controller = new TasksController(mockRepository.Object);
            var id = Guid.NewGuid();

            mockRepository.Setup(c => c.GetTaskById(id)).Throws<Exception>();

            //Act
            var result = controller.GetTaskById(id);

            //Assert
            var objectResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, objectResult.StatusCode);
            Assert.Equal("An unexpected error occurred.", objectResult.Value);
        }

        [Fact]
        public void CreateTask_ValidTask_CreatesTaskSuccessfully()
        {
            // Arrange
            var repository = new InMemoryRepository();
            var controller = new TasksController(repository);

            CreateTaskObjDto task = new()
            {
                Name = "Test",
                Priority = 1,
                Status = Status.NotStarted,
            };

            //Act
            var result = controller.CreateTask(task);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var taskResult = Assert.IsType<TaskObj>(okResult.Value);
            Assert.Equal(task.Name, taskResult.Name);
            Assert.Equal(task.Priority, taskResult.Priority);
            Assert.Equal(task.Status, taskResult.Status);
        }

        [Fact]
        public void CreateTask_TaskWithSameName_ReturnsBadRequest()
        {
            //Arrange
            var repository = new InMemoryRepository();
            var id = Guid.NewGuid();

            TaskObj task = new()
            {
                ID = id,
                Name = "SameNameTest",
                Priority = 1,
                Status = Status.NotStarted,
                CreatedDate = DateTime.Now,
            };

            repository.CreateTask(task);

            var controller = new TasksController(repository);

            CreateTaskObjDto createTaskDto = new()
            {
                Name = "SameNameTest",
                Priority = 1,
                Status = Status.Completed,
            };

            //Act
            var result = controller.CreateTask(createTaskDto);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UpdateTask_ValidTask_ReturnsNoContent()
        {
            //Arrange
            var repository = new InMemoryRepository();
            var guid1 = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D");
            var guid2 = new Guid("123e4567-e89b-12d3-a456-426614174000");

            TaskObj task1 = new()
            {
                ID = guid1,
                Name = "Test",
                Priority = 1,
                Status = Status.NotStarted,
                CreatedDate = DateTime.Now,
            };

            TaskObj task2 = new()
            {
                ID = guid2,
                Name = "Test2",
                Priority = 2,
                Status = Status.Completed,
                CreatedDate = DateTime.Now,
            };

            repository.CreateTask(task1);
            repository.CreateTask(task2);
            var controller = new TasksController(repository);

            UpdateTaskObjDto updateTaskDto = new()
            {
                Name = "NewTask",
                Priority = 1,
                Status = Status.NotStarted,
            };

            //Act
            var result = controller.UpdateTask(guid2, updateTaskDto);

            //Assert
            Assert.IsType<NoContentResult>(result);
            var updatedTask = repository.GetTaskById(guid2);
            Assert.Equal(updateTaskDto.Name, updatedTask.Name);
            Assert.Equal(updateTaskDto.Priority, updatedTask.Priority);
            Assert.Equal(updateTaskDto.Status, updatedTask.Status);
        }

        [Fact]
        public void UpdateTask_InvalidId_ReturnsNotFound()
        {
            //Arrange
            var repository = new InMemoryRepository();
            var validGuid = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D");
            var invalidGuid = new Guid("123e4567-e89b-12d3-a456-426614174000");

            TaskObj task1 = new()
            {
                ID = validGuid,
                Name = "Test",
                Priority = 1,
                Status = Status.NotStarted,
                CreatedDate = DateTime.Now,
            };

            repository.CreateTask(task1);
            var controller = new TasksController(repository);

            UpdateTaskObjDto updateTaskDto = new()
            {
                Name = "NewTask",
                Priority = 1,
                Status = Status.NotStarted,
            };

            //Act
            var result = controller.UpdateTask(invalidGuid, updateTaskDto);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void UpdateTask_UnexpectedError_ReturnsInternalServerError()
        {
            // Arrange
            var repository = new Mock<IRepository>();
            repository.Setup(r => r.GetTaskById(It.IsAny<Guid>())).Throws<Exception>();
            var controller = new TasksController(repository.Object);

            var id = Guid.NewGuid();
            UpdateTaskObjDto updateTaskDto = new()
            {
                Name = "NewTask",
                Priority = 1,
                Status = Status.NotStarted,
            };


            //Act
            var result = controller.UpdateTask(id, updateTaskDto);

            //Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
            Assert.Equal("An unexpected error occurred.", objectResult.Value);
        }

        [Fact]
        public void DeleteTask_CompletedTask_Success()
        {
            //Arrange
            var repository = new InMemoryRepository();
            var id = Guid.NewGuid();

            TaskObj task = new()
            {
                ID = id,
                Name = "SameNameTest",
                Priority = 1,
                Status = Status.Completed,
                CreatedDate = DateTime.Now,
            };

            repository.CreateTask(task);

            var controller = new TasksController(repository);

            //Act
            var result = controller.DeleteTask(id);

            //Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Throws<InvalidOperationException>(() => repository.GetTaskById(id));
        }

        [Fact]
        public void DeleteTask_NotCompletedTask_ReturnsBadRequest()
        {
            //Arrange
            var repository = new InMemoryRepository();
            var id = Guid.NewGuid();

            TaskObj task = new()
            {
                ID = id,
                Name = "SameNameTest",
                Priority = 1,
                Status = Status.InProgress,
                CreatedDate = DateTime.Now,
            };

            repository.CreateTask(task);

            var controller = new TasksController(repository);

            //Act
            var result = controller.DeleteTask(id);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void DeleteTask_TaskNotFound_ReturnsNotFound()
        {
            //Arrange
            var repository = new InMemoryRepository();
            var validGuid = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D");
            var invalidGuid = new Guid("123e4567-e89b-12d3-a456-426614174000");

            TaskObj task1 = new()
            {
                ID = validGuid,
                Name = "Test",
                Priority = 1,
                Status = Status.NotStarted,
                CreatedDate = DateTime.Now,
            };

            repository.CreateTask(task1);
            var controller = new TasksController(repository);

            //Act
            var result = controller.DeleteTask(invalidGuid);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteTask_UnexpectedError_ReturnsInternalServerError()
        {
            // Arrange
            var repository = new Mock<IRepository>();
            repository.Setup(r => r.GetTaskById(It.IsAny<Guid>())).Throws<Exception>();
            var controller = new TasksController(repository.Object);

            var id = Guid.NewGuid();

            //Act
            var result = controller.DeleteTask(id);

            //Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
            Assert.Equal("An unexpected error occurred.", objectResult.Value);
        }
    }
}
