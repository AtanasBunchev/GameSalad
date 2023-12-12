using Microsoft.AspNetCore.Mvc;
using GameSalad.Controllers;
using GameSalad.Entities;
using GameSalad.ExtensionMethods;
using GameSalad.ViewModels.FriendList;
using GameSaladTests.Repositories;

namespace GameSaladTests.Controllers;

public class FriendListControllerTests
{
    private TestFriendListController controller;
    private TestUsersDbContext context;

    private List<User> Followers = new List<User>();
    private List<User> Following = new List<User>();

    public FriendListControllerTests()
    {
        this.context = new TestUsersDbContext();
        this.controller = new TestFriendListController(context);

        User? loggedUser = null;
        for(int i = 1; i < 10; i++)
        {
            var user = UsersDbContextTests.GetValidUser();
            user.Username = $"user{i}";
            this.context.Add(user);

            if(loggedUser == null)
                loggedUser = user;

            if(i % 2 == 0)
            {
                Followers.Append(user);
                user.Follow(this.context, loggedUser);
            }
            if(i % 3 == 0)
            {
                Following.Append(user);
                loggedUser.Follow(this.context, user);
            }
        }

        this.context.SaveChanges();
        this.controller.LoggedUser = loggedUser;
    }


    [Fact]
    public void IndexDisplaysFollowingListsTest()
    {
        var result = Assert.IsType<ViewResult>(controller.Index());
        var model = Assert.IsType<IndexVM>(result.Model);

        var following = model.Following;

        Assert.Equal(Following.Count, following.Count);
        following.Sort(delegate(User x, User y)
        {
            return x.Id.CompareTo(y.Id);
        });
        Assert.Equal(Following, following);
    }

    [Fact]
    public void IndexDisplaysFollowersListsTest()
    {
        var result = Assert.IsType<ViewResult>(controller.Index());
        var model = Assert.IsType<IndexVM>(result.Model);

        var followers = model.Followers;

        Assert.Equal(Followers.Count, followers.Count);
        followers.Sort(delegate(User x, User y)
        {
            return x.Id.CompareTo(y.Id);
        });
        Assert.Equal(Followers, followers);
    }

}
