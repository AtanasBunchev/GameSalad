using GameSalad.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace GameSaladTests.Controllers;

public class HomeControllerTests
{
    private HomeController controller;

    public HomeControllerTests()
    {
        this.controller = new HomeController();
    }

    [Fact]
    public void IndexReturnsViewResultTest()
    {
        Assert.IsType<ViewResult>(controller.Index());
    }
}
