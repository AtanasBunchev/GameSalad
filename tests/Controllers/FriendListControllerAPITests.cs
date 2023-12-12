using Microsoft.AspNetCore.Mvc;
using GameSalad.Controllers;
using GameSalad.Entities;
using GameSalad.ExtensionMethods;
using GameSalad.ViewModels.FriendList;
using GameSaladTests.Repositories;

namespace GameSaladTests.Controllers;

public class FriendListControllerAPITests
{
    private TestFriendListController controller;
    private TestUsersDbContext context;

    public FriendListControllerAPITests()
    {
        this.context = new TestUsersDbContext();
        this.controller = new TestFriendListController(context);
    }


    [Fact]
    public void FollowAddesUserFollowEntryAndRedirectsTest()
    {
        var user1 = UsersDbContextTests.GetValidUser();
        user1.Username = "user1";
        this.context.Add(user1);

        var user2 = UsersDbContextTests.GetValidUser();
        user2.Username = "user2";
        this.context.Add(user2);

        this.context.SaveChanges();
        this.controller.LoggedUser = user1;

        var result = controller.Follow(user2.Username);
        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirect.ActionName);

        Assert.Empty(this.controller.UnfollowCalls);
        Assert.Single(this.controller.FollowCalls);
        Assert.Equal(user1, this.controller.FollowCalls[0].Item1);
        Assert.Equal(user2, this.controller.FollowCalls[0].Item2);
    }

    [Fact]
    public void UnfollowRemovesUserFollowEntryAndRedirectsTest()
    {
        var user1 = UsersDbContextTests.GetValidUser();
        user1.Username = "user1";
        this.context.Add(user1);

        var user2 = UsersDbContextTests.GetValidUser();
        user2.Username = "user2";
        this.context.Add(user2);

        this.context.SaveChanges();
        this.controller.LoggedUser = user1;

        var result = controller.Unfollow(user2.Username);
        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirect.ActionName);

        Assert.Empty(this.controller.FollowCalls);
        Assert.Single(this.controller.UnfollowCalls);
        Assert.Equal(user1, this.controller.UnfollowCalls[0].Item1);
        Assert.Equal(user2, this.controller.UnfollowCalls[0].Item2);
    }
}
