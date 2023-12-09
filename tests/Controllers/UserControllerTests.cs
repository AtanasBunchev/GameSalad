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
        ViewResult? result = controller.Login() as ViewResult;
        Assert.NotNull(result);
    }

    [Fact]
    public void SignUpReturnsViewResultTest()
    {
        ViewResult? result = controller.SignUp() as ViewResult;
        Assert.NotNull(result);
    }

    [Fact]
    public void SignUpWithInvalidModelReturnsViewTest()
    {
        SignUpVM model = SignUpVMTests.GetInvalidModel();
        ViewResult? result = controller.SignUp(model) as ViewResult;
        Assert.NotNull(result);
    }

    [Fact]
    public void SignUpWithValidModelRedirectsTest()
    {
        SignUpVM model = SignUpVMTests.GetValidModel();
        RedirectResult? result = controller.SignUp(model) as RedirectResult;
        Assert.NotNull(result);
    }

    [Fact]
    public void SignUpWithValidModelCreatesUserTest()
    {
        SignUpVM model = SignUpVMTests.GetValidModel();
        controller.SignUp(model);

        Assert.NotNull(model.Username);
        User? user = context.FindByUsername(model.Username);
        Assert.NotNull(user);
    }

    [Fact]
    public void SignUpWithInvalidModelDoNotCreateUserTest()
    {
        SignUpVM model = SignUpVMTests.GetInvalidModel();
        controller.SignUp(model);

        Assert.NotNull(model.Username);
        User? user = context.FindByUsername(model.Username);
        Assert.Null(user);
    }
}
