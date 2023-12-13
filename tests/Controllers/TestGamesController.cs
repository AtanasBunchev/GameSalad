using GameSalad.Controllers;
using GameSalad.Entities;
using GameSalad.Games;
using GameSalad.Repositories;
using GameSaladTests.Games;
using Microsoft.AspNetCore.Mvc;

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


    /* Mocking Finctionality Generic */

    public class PlayCallData
    {
        public Type Type = null!;
        public string View = null!;
        public string? Action;
        public IActionResult Result = null!;

        // Game object defined before the call
        public IGame? Game = null;
    }

    public Queue<PlayCallData> PlayCalls = new();

    public override IActionResult Play<T>(string view, string? action = null)
    {
        var result = View();

        PlayCalls.Enqueue(new PlayCallData
        {
            Type = typeof(T),
            View = view,
            Action = action,
            Result = result
        });

        return result;
    }
    public override IActionResult Play<T>(string view, T game, string? action = null)
    {
        var result = View();

        PlayCalls.Enqueue(new PlayCallData
        {
            Type = typeof(T),
            View = view,
            Action = action,
            Result = result,
            Game = game
        });

        return result;
    }



    /* Mocking Play */

    public MockGame Game = new();

    public IActionResult BasePlay(string? action = null, string view = "MockGame")
    {
        return base.Play<MockGame>(view, Game, action);
    }
}
