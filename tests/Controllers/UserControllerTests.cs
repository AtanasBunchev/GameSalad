using Microsoft.AspNetCore.Mvc;
using GameSalad.Controllers;
using GameSalad.ViewModels.User;
using GameSalad.Entities;
using GameSaladTests.Repositories;
using GameSaladTests.ViewModels.User;

namespace GameSaladTests.Controllers;

public class UserControllerTests
{
    private UserController controller;
    private TestUsersDbContext context;

    public UserControllerTests()
    {
        this.context = new TestUsersDbContext();
        this.controller = new UserController(context);
    }

    [Fact]
    public void LoginReturnsViewResultTest()
    {
        Assert.IsType<ViewResult>(controller.Login());
    }

    [Fact]
    public void SignUpReturnsViewResultTest()
    {
        Assert.IsType<ViewResult>(controller.SignUp());
    }

    [Fact]
    public void SignUpWithInvalidModelReturnsViewTest()
    {
        SignUpVM model = SignUpVMTests.GetValidModel();
        controller.ViewData.ModelState.AddModelError("Key", "Message");
        Assert.IsType<ViewResult>(controller.SignUp(model));
    }

    [Fact]
    public void SignUpWithValidModelRedirectsToLoginTest()
    {
        SignUpVM model = SignUpVMTests.GetValidModel();
        var result = Assert
            .IsType<RedirectToActionResult>(controller.SignUp(model));
        Assert.Null(result.ControllerName);
        Assert.Equal("Login", result.ActionName);
    }

    [Fact]
    public void SignUpWithValidModelCreatesUserTest()
    {
        SignUpVM model = SignUpVMTests.GetValidModel();
        controller.SignUp(model);

        Assert.NotNull(model.Username);
        var user = context.FindByUsername(model.Username);
        Assert.NotNull(user);
    }

    [Fact]
    public void SignUpWithInvalidModelDoNotCreateUserTest()
    {
        SignUpVM model = SignUpVMTests.GetValidModel();
        controller.ViewData.ModelState.AddModelError("Key", "Message");

        controller.SignUp(model);

        Assert.NotNull(model.Username);
        var user = context.FindByUsername(model.Username);
        Assert.Null(user);
    }

    [Fact]
    public void SignUpWithValidModelStoresUsernameForNextRequestTest()
    {
        SignUpVM model = SignUpVMTests.GetValidModel();
        controller.SignUp(model);

        Assert.NotNull(model.Username);
        Assert.Equal(model.Username, controller.CreatedUser);
    }

    [Fact]
    public void LoginShowsCreatedUsername()
    {
        string username = "user";
        controller.CreatedUser = username;
        var result = Assert.IsType<ViewResult>(controller.Login());
        var model = Assert.IsType<LoginVM>(result.Model);
        Assert.Equal(model.Username, username);
    }
}
