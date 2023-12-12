using GameSalad.Controllers;
using GameSalad.Repositories;
using GameSalad.Entities;

namespace GameSaladTests.Controllers;

public class TestFriendListController : FriendListController
{
    public User? LoggedUser { get; set; } = null;

    public TestFriendListController(UsersDbContext context)
        : base(context)
    {
    }

    protected override User? GetLoggedUser()
    {
        return this.LoggedUser;
    }
}
