using GameSalad.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace GameSaladTests.Controllers;

public class UserControllerTests
{
    private UserController controller;

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
    public void SignUpReturnsViewResultTest()
    {
        ViewResult? result = controller.SignUp() as ViewResult;
        Assert.NotNull(result);
    }
}
