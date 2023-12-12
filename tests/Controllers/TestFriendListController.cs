using GameSalad.Controllers;
using GameSalad.Repositories;
using GameSalad.Entities;
using GameSalad.ExtensionMethods;

namespace GameSaladTests.Controllers;

public class TestFriendListController : FriendListController
{
    public User? LoggedUser { get; set; } = null;

    public List<(User, User)> FollowCalls { get; set; } = new List<(User, User)>();
    public List<(User, User)> UnfollowCalls { get; set; } = new List<(User, User)>();

    public TestFriendListController(UsersDbContext context)
        : base(context)
    {
    }

    protected override User? GetLoggedUser()
    {
        return this.LoggedUser;
    }

    protected override void Follow(User user1, User user2)
    {
        FollowCalls.Add((user1, user2));
    }

    protected override void Unfollow(User user1, User user2)
    {
        UnfollowCalls.Add((user1, user2));
    }
}
