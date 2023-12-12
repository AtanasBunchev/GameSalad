using GameSalad.ExtensionMethods;
using GameSalad.Entities;
using GameSaladTests.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GameSaladTests.ExtensionMethods;

public class UserFriendListTest
{
    TestUsersDbContext context;
    User user1;
    User user2;

    public UserFriendListTest()
    {
        this.context = new TestUsersDbContext();

        this.user1 = UsersDbContextTests.GetValidUser();
        this.user1.Username = "user1";
        this.user2 = UsersDbContextTests.GetValidUser();
        this.user2.Username = "user2";

        this.context.Add(this.user1);
        this.context.Add(this.user2);
        this.context.SaveChanges();
    }

    [Fact]
    public void UserFollowTest()
    {
        user1.Follow(this.context, user2);

        var entries = this.context.UserFollowEntries.ToList();

        Assert.Equal(1, entries.Count());
        var entry = entries[0];

        context.Entry(entry)
            .Reference(e => e.Follower)
            .Load();
        context.Entry(entry)
            .Reference(e => e.Target)
            .Load();

        Assert.Equal(user1, entry.Follower);
        Assert.Equal(user2, entry.Target);
    }
}
