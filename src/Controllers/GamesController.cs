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
        return Play<TicTacToe>("TicTacToe", action);
    }

    public IActionResult TwentyFortyEight(string? action = null)
    {
        //return Play<TwentyFortyEight>("TwentyFortyEight", game, action);
        return View();
    }

    [NonAction]
    public virtual IActionResult Play<T>(string view, string? move = null)
        where T : IGame, new()
    {
        T game = new();
        return Play<T>(view, game, move);
    }

    [NonAction]
    public virtual IActionResult Play<T>(string view, T game, string? move = null)
        where T : IGame, new()
    {
        var user = GetLoggedUser();
        if (user == null)
            return Redirect("/");

        var entry = context.Games
            .Where(g =>
                g.UserId == user.Id
                && g.Active == true
                && g.Type == game.GetGameType())
            .FirstOrDefault();

        if (entry == null)
        {
            entry = new GameEntry
            {
                Type = game.GetGameType(),
                UserId = user.Id,
                Data = game.GetState()
            };
            context.Add(entry);
            context.SaveChanges();
        }
        else if (entry.Data != null)
        {
            game.SetState(entry.Data);
        }

        if (move != null)
        {
            var validMoves = game.GetValidMoves();
            if (validMoves.Contains(move))
            {
                game.PlayMove(move);

                entry.Data = game.GetState();
                context.Update(entry);
                context.SaveChanges();
                if (game.HasFinished())
                {
                    return RedirectToAction("GameStats", new {
                        id = entry.Id
                    });
                }
                return RedirectToAction(view);
            }
        }

        return View(view, game);
    }


    public IActionResult GameStats(int id)
    {
        // TODO test & implement
        return View();
    }
}
