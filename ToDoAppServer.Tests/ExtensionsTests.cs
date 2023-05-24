using System;
using System.Collections.Generic;
using ToDoAppServer.Extensions;
using ToDoAppServer.Models;

namespace ToDoAppServer.Tests
{
    public class ExtensionsTests
    {
        [Fact]
        public void AsDto_ReturnsTaskObjDto()
        {
            //Arrange
            var id = Guid.NewGuid();
            TaskObj task = new()
            {
                ID = id,
                Name = "Test",
                Priority = 1,
                Status = Status.NotStarted,
                CreatedDate = DateTime.Now,
            };

            //Act
            var taskObjDto = task.AsDto();

            //Assert
            Assert.NotNull(taskObjDto);
            Assert.Equal(task.ID, taskObjDto.ID);
            Assert.Equal(task.Name, taskObjDto.Name);
            Assert.Equal(task.Priority, taskObjDto.Priority);
            Assert.Equal(task.Status, taskObjDto.Status);
            Assert.Equal(task.CreatedDate, taskObjDto.CreatedDate);
        }
    }
}
