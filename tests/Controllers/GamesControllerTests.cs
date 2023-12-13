using Microsoft.AspNetCore.Mvc;
using GameSalad.Controllers;
using GameSalad.Entities;
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


    /* Basic view tests */

    [Fact]
    public void IndexReturnsViewResultTest()
    {
        Assert.IsType<ViewResult>(controller.Index());
    }

    /* Game Interface tests */
}
