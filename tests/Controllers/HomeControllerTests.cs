using GameSalad.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace GameSaladTests.Controllers;

public class HomeControllerTests
{
    [Fact]
    public void IndexSetsTitleTest()
    {
        HomeController controller = new HomeController();
        ViewResult? result = controller.Index() as ViewResult;
        if (result == null)
        {
            Assert.Fail("Expected ViewResult as return type!");
        }
        else
        {
            Assert.Equal("GameSalad | Home", result.ViewData["Title"]);
        }
    }
}
