using GameSalad.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace GameSaladTests.Controllers;

public class UserControllerTests
{
    UserController controller;
    public UserControllerTests()
    {
        this.controller = new UserController();
    }

    [Fact]
    public void LoginReturnsViewResultTest()
    {
        ViewResult? result = controller.Login() as ViewResult;
        Assert.NotNull(result);
    }

    [Fact]
    public void RegisterReturnsViewResultTest()
    {
        ViewResult? result = controller.Register() as ViewResult;
        Assert.NotNull(result);
    }
}
