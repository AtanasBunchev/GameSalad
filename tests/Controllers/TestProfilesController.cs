using GameSalad.Controllers;
using GameSalad.Repositories;
using GameSalad.Entities;
using GameSalad.ExtensionMethods;

namespace GameSaladTests.Controllers;

public class TestProfilesController : ProfilesController
{
    public User? LoggedUser { get; set; } = null;

    public TestProfilesController(UsersDbContext context)
        : base(context)
    {
    }

    protected override User? GetLoggedUser()
    {
        return this.LoggedUser;
    }
}
