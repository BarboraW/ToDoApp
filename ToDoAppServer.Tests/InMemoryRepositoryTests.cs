using ToDoAppServer.Models;
using ToDoAppServer.Repositories;

namespace ToDoAppServer.Tests
{
    public class InMemoryRepositoryTests
    {
        [Fact]
        public void GetAllTasks_ReturnsAllTasks()
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

            //Act
            var result = repository.GetAllTasks();

            //Assert
            Assert.Collection(result,
                 task => Assert.Equal(task1, task),
                 task => Assert.Equal(task2, task)
            );
        }

        [Fact]
        public void GetAllTasks_ReturnsEmptyList_WhenNoTasksExist()
        {
            // Arrange
            var repository = new InMemoryRepository();

            // Act
            var result = repository.GetAllTasks();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetTaskById_ReturnsTask()
        {
            //Arrange
            var repository = new InMemoryRepository();
            var guid = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D");

            TaskObj task = new()
            {
                ID = guid,
                Name = "Test",
                Priority = 1,
                Status = Status.NotStarted,
                CreatedDate = DateTime.Now,
            };

            repository.CreateTask(task);

            //Act
            var result = repository.GetTaskById(guid);

            //Assert
            Assert.Equal(task, result);
        }

        [Fact]
        public void GetTaskById_TaskNotFound()
        {
            //Arrange
            var repository = new InMemoryRepository();
            var guid = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D");

            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => repository.GetTaskById(guid));

            //Assert
            Assert.Throws<InvalidOperationException>(() => repository.GetTaskById(guid));
            Assert.Equal("Task not found.", exception.Message);
        }

        [Fact]
        public void UpdateTask_ShouldUpdateTaskInList()
        {
            // Arrange
            var repository = new InMemoryRepository();
            var tasks = new List<TaskObj>
            {
                new TaskObj { ID = Guid.NewGuid(), Name = "Task 1", Priority = 1, Status = Status.NotStarted },
                new TaskObj { ID = Guid.NewGuid(), Name = "Task 2", Priority = 2, Status = Status.InProgress },
                new TaskObj { ID = Guid.NewGuid(), Name = "Task 3", Priority = 3, Status = Status.Completed }
            };

            foreach (var task in tasks)
            {
                repository.CreateTask(task);
            }

            var taskToUpdate = new TaskObj { ID = tasks[1].ID, Name = "Updated Task", Priority = 4, Status = Status.Completed };

            // Act
            repository.UpdateTask(taskToUpdate);

            // Assert
            var updatedTask = repository.GetTaskById(taskToUpdate.ID);
            Assert.Equal(taskToUpdate, updatedTask);
        }

        [Fact]
        public void CreateTask_ShouldCreateTask()
        {
            //Arrange
            var repository = new InMemoryRepository();

            var guid = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D");

            TaskObj task = new()
            {
                ID = guid,
                Name = "Test",
                Priority = 1,
                Status = Status.NotStarted,
                CreatedDate = DateTime.Now,
            };

            //Act
            repository.CreateTask(task);

            //Assert
            var createdTask = repository.GetTaskById(guid);
            Assert.Equal(task, createdTask);
        }

        [Fact]
        public void DeleteTask_ShouldDeleteTask()
        {
            //Arrange
            var repository = new InMemoryRepository();

            var guid = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D");

            TaskObj task = new()
            {
                ID = guid,
                Name = "Test",
                Priority = 1,
                Status = Status.NotStarted,
                CreatedDate = DateTime.Now,
            };

            repository.CreateTask(task);

            //Act
            repository.DeleteTask(guid);

            //Assert
            Assert.Throws<InvalidOperationException>(() => repository.GetTaskById(guid));
        }
    }
}