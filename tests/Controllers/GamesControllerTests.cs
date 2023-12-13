using Microsoft.AspNetCore.Mvc;
using GameSalad.Controllers;
using GameSalad.Entities;
using GameSalad.Games;
using GameSaladTests.Repositories;
using GameSaladTests.Games;

namespace GameSaladTests.Controllers;

public class GamesControllerTests
{
    private TestGamesController controller;
    private TestUsersDbContext context;
    private User user;

    public GamesControllerTests()
    {
        this.context = new TestUsersDbContext();
        this.controller = new TestGamesController(context);
        this.user = UsersDbContextTests.GetValidUser();
        this.controller.LoggedUser = this.user;
        context.Add(this.user);
        context.SaveChanges();
    }


    /* Basic View Tests */

    [Fact]
    public void IndexReturnsViewResultTest()
    {
        Assert.IsType<ViewResult>(controller.Index());
    }


    /* Public Views Tests */

    [Fact]
    public void TicTacToeCallsPlayTest()
    {
        // Without action
        var result = controller.TicTacToe();
        Assert.Single(controller.PlayCalls);

        var call = controller.PlayCalls.Dequeue();
        Assert.Equal(typeof(TicTacToe), call.Type);
        Assert.Equal("TicTacToe", call.View);
        Assert.Null(call.Action);
        Assert.Equal(result, call.Result);

        // With action
        string action = "TestAction";
        result = controller.TicTacToe(action);
        Assert.Single(controller.PlayCalls);

        call = controller.PlayCalls.Dequeue();
        Assert.Equal(typeof(TicTacToe), call.Type);
        Assert.Equal("TicTacToe", call.View);
        Assert.Equal(action, call.Action);
        Assert.Equal(result, call.Result);
    }

    /* Missing GameSalad.Games.TwentyFortyEight
    [Fact]
    public void TwentyFortyEightCallsPlayTest()
    {
        // Without action
        var result = controller.TwentyFortyEight();
        Assert.Single(controller.PlayCalls);

        var call = controller.PlayCalls.Dequeue();
        Assert.Equal(typeof(TwentyFortyEight), call.Type);
        Assert.Equal("TwentyFortyEight", call.View);
        Assert.Null(call.Action);
        Assert.Equal(result, call.Result);
        Assert.Null(call.Game);

        // With action
        string action = "TestAction";
        result = controller.TwentyFortyEight(action);
        Assert.Single(controller.PlayCalls);

        call = controller.PlayCalls.Dequeue();
        Assert.Equal(typeof(TwentyFortyEight), call.Type);
        Assert.Equal("TwentyFortyEight", call.View);
        Assert.Equal(action, call.Action);
        Assert.Equal(result, call.Result);
        Assert.Null(call.Game);
    }
    */


    /* Game Interface tests */
    [Fact]
    public void PlayLoggedUserNotFoundRedirectToBaseTest()
    {
        controller.LoggedUser = null;
        var result = controller.BasePlay();
        var redirect = Assert.IsType<RedirectResult>(result);
        Assert.Equal("/", redirect.Url);
    }

    [Fact]
    public void PlayCreateActiveGameIfNotExistsTest()
    {
        var state = "Custom Game State Data";
        MockGame game = controller.Game;
        game.State = state;

        controller.BasePlay();

        var entries = context.Games.ToList();
        Assert.Single(entries);
        var entry = entries[0];

        Assert.Equal(game.GetGameType(), entry.Type);
        Assert.Equal(user.Id, entry.UserId);
        Assert.Equal(state, entry.Data);
    }

    [Fact]
    public void PlayLoadActiveGameIfExistsTest()
    {
        var state = "Custom Game State Data";

        MockGame game = controller.Game; // default initialized
        var entry = new GameEntry
        {
            Type = game.GetGameType(),
            UserId = user.Id,
            Data = state
        };
        context.Add(entry);
        context.SaveChanges();

        controller.BasePlay();

        Assert.Single(context.Games.ToList());
        Assert.Equal(state, game.State);
    }

    [Fact]
    public void PlayNoMoveReturnsViewToGamePageTest()
    {
        var result = controller.BasePlay();
        var view = Assert.IsType<ViewResult>(result);
        Assert.Equal("MockGame", view.ViewName);
        Assert.Equal(controller.Game, view.Model);
    }

    [Fact]
    public void PlayValidMoveReturnRedirectToSelfAndCallGameMoveTest()
    {
        var move = "Test!";
        string viewName = "myView";

        var game = controller.Game;
        game.ValidMoves.Add(move);

        var result = controller.BasePlay(move, view: viewName);
        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(viewName, redirect.ActionName);
    }

    [Fact]
    public void PlayInvalidMoveReturnViewAndDoNotCallGameMoveTest()
    {
        var move = "Test!";
        string viewName = "myView";

        var game = controller.Game;
        // game.ValidMoves.Add(move); -- Not a valid move

        var result = controller.BasePlay(move, view: viewName);
        var view = Assert.IsType<ViewResult>(result);
        Assert.Equal(viewName, view.ViewName);
    }


    [Fact]
    public void PlayValidMoveChangesAndUpdatesStateTest()
    {
        var state1 = "InitialState";
        var game = controller.Game;
        game.State = state1;

        // Create game
        controller.BasePlay();

        var entries = context.Games.ToList();
        Assert.Single(entries);
        var entry = entries[0];

        Assert.Equal(state1, entry.Data);
        controller.Game = new MockGame();
        game = controller.Game;


        // Do move that changes action
        var move = "Test!";
        var state2 = "NewState";
        game.ValidMoves.Add(move);
        game.NextState = state2;

        controller.BasePlay(move);

        Assert.Equal(state2, game.State);
        Assert.Equal(state2, entry.Data);
    }

    [Fact]
    public void PlayInvalidMoveDoNotChangesStateTest()
    {
        var state1 = "InitialState";
        var game = controller.Game;
        game.State = state1;

        // Create game
        controller.BasePlay();

        var entries = context.Games.ToList();
        Assert.Single(entries);
        var entry = entries[0];

        Assert.Equal(state1, entry.Data);
        controller.Game = new MockGame();
        game = controller.Game;


        // Do move that changes action
        var move = "Test!";
        var state2 = "NewState";
        // game.ValidMoves.Add(move); -- Not a valid move
        game.NextState = state2;

        controller.BasePlay(move);

        Assert.Equal(state1, game.State);
        Assert.Equal(state1, entry.Data);
    }

    [Fact]
    public void PlayGameFinishesRedirectToGameStatsScreen()
    {
        var move = "finish";
        var game = controller.Game;
        game.NextFinished = true;
        game.ValidMoves.Add("finish");

        var result = controller.BasePlay(move);
        var redirect = Assert.IsType<RedirectToActionResult>(result);

        var entries = context.Games.ToList();
        Assert.Single(entries);
        var entry = entries[0];

        Assert.Equal("GameStats", redirect.ActionName);
        int id = entry.Id;
        var dictionary = redirect.RouteValues;
        Assert.NotNull(dictionary);
        Assert.NotNull(dictionary["Id"]);
        Assert.Equal(id, dictionary["Id"]);
    }

    [Fact]
    public void GameStatsRedirectsIfGameNotFoundTest()
    {
        var result = controller.GameStats(10);
        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirect.ActionName);
    }

    [Fact]
    public void GameStatsSetsGameAsModelAndGameTypeAsViewTest()
    {
        // Create game
        controller.BasePlay();

        var entries = context.Games.ToList();
        Assert.Single(entries);
        var entry = entries[0];

        var result = controller.GameStats(entry.Id);
        var view = Assert.IsType<ViewResult>(result);
        Assert.Equal(entry, view.Model);
        Assert.Equal(controller.Game.GetGameType(), view.ViewName);
    }
}
