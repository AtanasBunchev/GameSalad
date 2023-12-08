using GameSalad.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace GameSaladTests;

public class HomeControllerTests
{
    [Fact]
    public void IndexSetsTitleTest()
    {
        HomeController controller = new HomeController();
        ViewResult result = controller.Index() as ViewResult;
        Assert.Equal("GameSalad | Home", result.ViewData["Title"]);
    }
}
