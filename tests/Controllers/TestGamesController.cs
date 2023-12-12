using GameSalad.Controllers;
using GameSalad.Repositories;
using GameSalad.Entities;

namespace GameSaladTests.Controllers;

public class TestGamesController : GamesController
{
    public User? LoggedUser { get; set; } = null;

    public TestGamesController(UsersDbContext context)
        : base(context)
    {
    }

    protected override User? GetLoggedUser()
    {
        return this.LoggedUser;
    }
}
