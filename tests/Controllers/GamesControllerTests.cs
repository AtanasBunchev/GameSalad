using Microsoft.AspNetCore.Mvc;
using GameSalad.Controllers;
using GameSalad.Entities;
using GameSalad.Games;
using GameSaladTests.Repositories;

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

    /* Missing GameSalad.Games.TicTacToe
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
    */

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

        // With action
        string action = "TestAction";
        result = controller.TwentyFortyEight(action);
        Assert.Single(controller.PlayCalls);

        call = controller.PlayCalls.Dequeue();
        Assert.Equal(typeof(TwentyFortyEight), call.Type);
        Assert.Equal("TwentyFortyEight", call.View);
        Assert.Equal(action, call.Action);
        Assert.Equal(result, call.Result);
    }
    */


    /* Game Interface tests */
}
