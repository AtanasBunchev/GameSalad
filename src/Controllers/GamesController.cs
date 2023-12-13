using GameSalad.Entities;
using GameSalad.Games;
using GameSalad.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameSalad.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class GamesController : CustomController
{
    public GamesController(UsersDbContext context)
        : base(context)
    {
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult TicTacToe(string? action = null)
    {
        //return Play<TicTacToe>("TwentyFortyEight", game, action);
        return View();
    }

    public IActionResult TwentyFortyEight(string? action = null)
    {
        //return Play<TwentyFortyEight>("TwentyFortyEight", game, action);
        return View();
    }

    [NonAction]
    public virtual IActionResult Play<T>(string view, string? action = null)
        where T : IGame, new()
    {
        T game = new();
        // TODO split, test & implement
        return View(view, game);
    }
}
