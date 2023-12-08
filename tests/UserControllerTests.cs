using GameSalad.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace GameSaladTests;

public class UserControllerTests
{
    [Fact]
    public void LoginSetsTitleTest()
    {
        UserController controller = new UserController();
        ViewResult result = controller.Login() as ViewResult;
        Assert.Equal("GameSalad | Login", result.ViewData["Title"]);
    }

    [Fact]
    public void RegisterSetsTitleTest()
    {
        UserController controller = new UserController();
        ViewResult result = controller.Register() as ViewResult;
        Assert.Equal("GameSalad | Register", result.ViewData["Title"]);
    }
}
