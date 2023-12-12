using Microsoft.AspNetCore.Mvc;
using GameSalad.Controllers;
using GameSalad.ViewModels.User;
using GameSalad.Entities;
using GameSaladTests.Repositories;
using GameSaladTests.ViewModels.User;

namespace GameSaladTests.Controllers;

public class UserControllerTests
{
    private TestUserController controller;
    private TestUsersDbContext context;

    public UserControllerTests()
    {
        this.context = new TestUsersDbContext();
        this.controller = new TestUserController(context);
    }


    /* Basic view tests */

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


    /* SignUp - Validation and user creation */

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
    public void SignUpWithInvalidModelDoesNotCreateUserTest()
    {
        SignUpVM model = SignUpVMTests.GetValidModel();
        controller.ViewData.ModelState.AddModelError("Key", "Message");

        controller.SignUp(model);

        Assert.NotNull(model.Username);
        var user = context.FindByUsername(model.Username);
        Assert.Null(user);
    }

    [Fact]
    public void SignUpWithExistingUsernameDoesNotCreateUserTest()
    {
        string username = "user";

        SignUpVM model = SignUpVMTests.GetValidModel();
        User item = UsersDbContextTests.GetValidUser();
        model.Username = username;
        item.Username = username;

        context.Add(item);
        context.SaveChanges();

        controller.SignUp(model);
        var user_count = context.Users
            .Where(u => u.Username == username)
            .ToList().Count();

        Assert.Equal(1, user_count);
    }

    [Fact]
    public void SignUpWithExistingUsernameDoesShowErrorMessageTest()
    {
        string username = "user";

        SignUpVM model = SignUpVMTests.GetValidModel();
        User item = UsersDbContextTests.GetValidUser();
        model.Username = username;
        item.Username = username;

        context.Add(item);
        context.SaveChanges();

        var result = controller.SignUp(model);
        Assert.True(controller.ModelState.ContainsKey("Username"));
    }


    /* Temp data transfer */

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
        Assert.Equal(username, model.Username);
    }


    /* Login tests - Validation and login */

    [Fact]
    public void LoginWithInvalidModelReturnsViewTest()
    {
        var model = LoginVMTests.GetValidModel();
        controller.ViewData.ModelState.AddModelError("Key", "Message");

        var result = controller.Login(model);
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void LoginWithValidModelWithoutExistingUserFailsTest()
    {
        var model = LoginVMTests.GetValidModel();
        var result = controller.Login(model);
        Assert.IsType<ViewResult>(result);
        Assert.True(controller.ModelState.ContainsKey("authError"),
            "Expected authError error in ModelState");
    }

    [Fact]
    public void LoginWithValidModelAndExistingUserRedirectsToBaseTest()
    {
        var model = LoginVMTests.GetValidModel();

        var user = UsersDbContextTests.GetValidUser();
        user.Username = model.Username;
        user.Password = model.Password;

        context.Add(user);
        context.SaveChanges();

        var result = controller.Login(model);
        var redirect = Assert.IsType<RedirectResult>(result);
        Assert.Equal("/", redirect.Url);
    }

    [Fact]
    public void SuccessfulLoginSetsAuthorizationTokenTest()
    {
        var model = LoginVMTests.GetValidModel();

        var user = UsersDbContextTests.GetValidUser();
        user.Username = model.Username;
        user.Password = model.Password;

        context.Add(user);
        context.SaveChanges();

        controller.Login(model);
        Assert.True(controller.BearerTokenModifiedCount > 0,
            "Expected SetAuthenticationBearerToken to be called.");
    }

    [Fact]
    public void LoginWithInvalidModelDoesNotSetAuthorizationTokenTest()
    {
        var model = LoginVMTests.GetValidModel();
        controller.ViewData.ModelState.AddModelError("Key", "Message");

        controller.Login(model);
        Assert.True(controller.BearerTokenModifiedCount == 0,
            "Expected SetAuthenticationBearerToken to not be called.");
    }

    [Fact]
    public void LoginWithoutExistingUserDoesNotSetAuthorizationTokenTest()
    {
        var model = LoginVMTests.GetValidModel();
        var result = controller.Login(model);
        Assert.True(controller.BearerTokenModifiedCount == 0,
            "Expected SetAuthenticationBearerToken to not be called.");
    }


}
