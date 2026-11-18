
using Microsoft.AspNetCore.Mvc;
using MiniTask.API.Controllers;
using MiniTask.API.Models;
using Xunit;

namespace MiniTask.Tests;

public class TasksControllerTests
{
    [Fact]
    public void GetById_ExistingTask_ReturnsOk()
    {
        // Arrange
        var controller = new TasksController();

        // Act
        var result = controller.GetById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);

        var task = Assert.IsType<TaskItem>(okResult.Value);

        Assert.Equal(1, task.Id);
        Assert.Equal("Learn CI/CD", task.Title);
    }

    [Fact]
    public void GetById_NonExistingTask_ReturnsNotFound()
    {
        // Arrange
        var controller = new TasksController();

        // Act
        var result = controller.GetById(99999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Create_NewTask_ReturnsCreated()
    {
        // Arrange
        var controller = new TasksController();

        var newTask = new TaskItem
        {
            Title = "Test automated testing",
            IsCompleted = false
        };

        // Act
        var result = controller.Create(newTask);

        // Assert
        var createdResult =
            Assert.IsType<CreatedAtActionResult>(result);

        var createdTask =
            Assert.IsType<TaskItem>(createdResult.Value);

        Assert.True(createdTask.Id > 0);
        Assert.Equal("Test automated testing", createdTask.Title);

        // Clean up the created task
        var deleteResult = controller.Delete(createdTask.Id);

        Assert.IsType<NoContentResult>(deleteResult);
    }
}
