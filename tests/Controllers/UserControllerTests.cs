using GameSalad.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace GameSaladTests.Controllers;

public class UserControllerTests
{
    [Fact]
    public void LoginSetsTitleTest()
    {
        UserController controller = new UserController();
        ViewResult? result = controller.Login() as ViewResult;
        if (result == null)
        {
            Assert.Fail("Expected ViewResult as return type!");
        }
        else
        {
            Assert.Equal("GameSalad | Login", result.ViewData["Title"]);
        }
    }

    [Fact]
    public void RegisterSetsTitleTest()
    {
        UserController controller = new UserController();
        ViewResult? result = controller.Register() as ViewResult;
        if (result == null)
        {
            Assert.Fail("Expected ViewResult as return type!");
        }
        else
        {
            Assert.Equal("GameSalad | Register", result.ViewData["Title"]);
        }
    }
}
