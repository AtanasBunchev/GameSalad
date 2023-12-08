using GameSalad.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace GameSaladTests.Controllers;

public class HomeControllerTests
{
    HomeController controller;
    public HomeControllerTests()
    {
        this.controller = new HomeController();
    }

    [Fact]
    public void IndexReturnsViewResultTest()
    {
        ViewResult? result = controller.Index() as ViewResult;
        Assert.NotNull(result);
    }
}
