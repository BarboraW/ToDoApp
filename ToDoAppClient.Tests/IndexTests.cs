using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using System.Net;
using ToDoAppClient.Models;
using ToDoAppClient.Pages;
using ToDoAppClient.Wrappers;

namespace ToDoAppClient.Tests
{
    public class IndexTests
    {
        [Fact]
        public async Task OnPostAsync_ValidModel_ReturnsPage()
        {
            // Arrange
            var httpClientWrapperMock = new Mock<IHttpClientWrapper>();
            var pageModel = new TasksListModel(httpClientWrapperMock.Object);

            var expectedResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("Success"),
            };

            httpClientWrapperMock
                .Setup(mock => mock.PostAsJsonAsync(It.IsAny<string>(), It.IsAny<TaskModel>()))
                .ReturnsAsync(expectedResponse);

            httpClientWrapperMock
                .Setup(mock => mock.GetAsync("https://localhost:44356/tasks"))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("[{\"Id\":\"01234567-89AB-CDEF-0123-456789ABCDEF\",\"Name\":\"Task 1\"}]"),
                });

            // Act
            var result = await pageModel.OnPostAsync();

            // Assert
            Assert.IsType<PageResult>(result);
            httpClientWrapperMock.Verify(mock => mock.GetAsync("https://localhost:44356/tasks"), Times.Once);
        }
    }
}