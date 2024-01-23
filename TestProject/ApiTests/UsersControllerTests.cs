using api.Controllers;
using infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using service.Services.Interface;

namespace TestProject.ApiTests;

[TestFixture]
public class UsersControllerTests
{
    private Mock<IUserService> _mockUserService;
    private UsersController _controller;

    [SetUp]
    public void SetUp()
    {
        
        _mockUserService = new Mock<IUserService>();
        _controller = new UsersController(_mockUserService.Object);
        
    }

    [Test]
    public void Get_UserExist_ReturnsOkResult()
    {
        // Arrange
        var userId = 1;
        var user = new Users { UserId = userId };
        
        /* Configure the mock object to setup a specific behavior when "getUserById" method is called with argument "userId"
         "Returns(user)" method returns the user object when the "Setup" condition is met */
        _mockUserService.Setup(s => s.getUserById(userId)).Returns(user);
        
        // Act
        var result = _controller.Get(userId);
        
        // Assert
        // Asserting that the result we got from the method is an OkObjectResult
        Assert.That(result, Is.TypeOf<OkObjectResult>());
        
        // Convert our result into an OkObjectResult so we can look at it's value
        var okResult = result as OkObjectResult;
        
        // Assert that the value inside our OkObjectResult is the same as the user we expected
        Assert.That(okResult.Value, Is.EqualTo(user));
    }
    [Test]
    public void Get_UserDoesNotExist_ReturnsNotFoundResult()
    {
        // Arrange
        var userId = 1;
        _mockUserService.Setup(s => s.getUserById(userId)).Returns((Users)null);

        // Act
        var result = _controller.Get(userId);

        // Assert
        Assert.That(result, Is.TypeOf<NotFoundResult>());
    }
    
    [Test]
    public void Create_ValidUser_ReturnsOkResult()
    {
        // Arrange
        var user = new Users { UserId = 1 };
        _mockUserService.Setup(s => s.createUser(user));

        // Act
        var result = _controller.Create(user);

        // Assert
        Assert.That(result, Is.TypeOf<OkObjectResult>());
        var okResult = result as OkObjectResult;
        Assert.That(okResult.Value, Is.EqualTo(user));
    }
    [Test]
    public void Create_NullUser_ReturnsBadRequestResult()
    {
        // Arrange
        Users user = null;

        // Act
        var result = _controller.Create(user);

        // Assert
        Assert.That(result, Is.TypeOf<BadRequestResult>());
    }
    
    [Test]
    public void Update_ValidUser_ReturnsOkResult()
    {
        // Arrange
        var user = new Users { UserId = 1 };
        _mockUserService.Setup(s => s.updateUser(user));

        // Act
        var result = _controller.Update(user.UserId, user);

        // Assert
        Assert.That(result, Is.TypeOf<OkObjectResult>());
        var okResult = result as OkObjectResult;
        Assert.That(okResult.Value, Is.EqualTo(user));
    }
    [Test]
    public void Update_NullUser_ReturnsBadRequestResult()
    {
        // Arrange
        Users user = null;

        // Act
        var result = _controller.Update(1, user);

        // Assert
        Assert.That(result, Is.TypeOf<BadRequestResult>());
    }

    [Test]
    public void Delete_UserExists_ReturnsOkResult()
    {
        // Arrange
        var userId = 1;
        var user = new Users { UserId = userId };
        
        // To mimic the controllers Delete method behavior we call the "getUserById" method
        // When "getUserById" method is called with argument userId returns a user object
        _mockUserService.Setup(s => s.getUserById(userId)).Returns(user);
        // When "deleteUser" is called with argument userId do nothing (Delete function returns no value)
        _mockUserService.Setup(s => s.deleteUser(userId));

        // Act
        var result = _controller.Delete(userId);

        // Assert
        Assert.That(result, Is.TypeOf<OkObjectResult>());
        var okResult = result as OkObjectResult;
        Assert.That(okResult.Value, Is.EqualTo(userId));
    }
    [Test]
    public void Delete_UserDoesNotExist_ReturnsNotFoundResult()
    {
        // Arrange
        var userId = 1;
        _mockUserService.Setup(s => s.getUserById(userId)).Returns((Users)null);

        // Act
        var result = _controller.Delete(userId);

        // Assert
        Assert.That(result, Is.TypeOf<NotFoundResult>());
    }
    

}