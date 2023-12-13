using Microsoft.AspNetCore.Mvc;
using GameSalad.Controllers;
using GameSalad.Entities;
using GameSalad.ExtensionMethods;
using GameSaladTests.Repositories;

namespace GameSaladTests.Controllers;

public class ProfilesControllerTests
{
    private TestProfilesController controller;
    private TestUsersDbContext context;
    private User user;

    public ProfilesControllerTests()
    {
        this.context = new TestUsersDbContext();
        this.controller = new TestProfilesController(context);

        this.user = UsersDbContextTests.GetValidUser();
        this.context.Add(user);
        this.context.SaveChanges();

        this.controller.LoggedUser = user;
    }

    [Fact]
    public void IndexWhenNotLoggedRedirectToBaseTest()
    {
        controller.LoggedUser = null;
        var result = controller.Index();
        var redirect = Assert.IsType<RedirectResult>(result);
        Assert.Equal("/", redirect.Url);
    }

    [Fact]
    public void IndexWithIdWhenNotLoggedRedirectToBaseTest()
    {
        controller.LoggedUser = null;
        var result = controller.Index(user.Id);
        var redirect = Assert.IsType<RedirectResult>(result);
        Assert.Equal("/", redirect.Url);
    }

    [Fact]
    public void IndexWithIdWhenNotFoundRedirectToBaseTest()
    {
        var result = controller.Index(user.Id + 1);
        var redirect = Assert.IsType<RedirectResult>(result);
        Assert.Equal("/", redirect.Url);
    }

    [Fact]
    public void IndexReturnsViewTest()
    {
        var result = controller.Index();
        var view = Assert.IsType<ViewResult>(result);
        Assert.Equal(user, Assert.IsType<User>(view.Model));
    }

    [Fact]
    public void IndexWithIdReturnsViewTest()
    {
        var otherUser = UsersDbContextTests.GetValidUser();
        this.context.Add(otherUser);
        this.context.SaveChanges();

        var result = controller.Index(otherUser.Id);
        var view = Assert.IsType<ViewResult>(result);
        Assert.Equal(otherUser, Assert.IsType<User>(view.Model));
    }

    [Fact]
    public void IndexModelHasListOfGamesTest()
    {
        var result = controller.Index();
        var view = Assert.IsType<ViewResult>(result);
        var item = Assert.IsType<User>(view.Model);
        Assert.True(context.Entry(item)
            .Collection(p => p.Games).IsLoaded,
            "Expected Games collection to be loaded");
    }

    [Fact]
    public void IndexWithIdHasListOfGamesTest()
    {
        var otherUser = UsersDbContextTests.GetValidUser();
        this.context.Add(otherUser);
        this.context.SaveChanges();

        var result = controller.Index(otherUser.Id);
        var view = Assert.IsType<ViewResult>(result);
        var item = Assert.IsType<User>(view.Model);
        Assert.True(context.Entry(item)
            .Collection(p => p.Games).IsLoaded,
            "Expected Games collection to be loaded");
    }
}
