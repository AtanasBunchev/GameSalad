using GameSalad.Entities;
using GameSalad.Games;
using GameSalad.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameSalad.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GameController : CustomController
    {
        public GameController(UsersDbContext context)
            : base(context)
        {
        }

        public IActionResult Index()
        {
            return RedirectToAction("Games", "Home");
        }

        public IActionResult TicTacToe(string move)
        {
            TicTacToe game = new TicTacToe();
            ProcessGame(game, move);
            if (move == null)
            {
                return View(game);
            }
            return RedirectToAction("TicTacToe");
        }

        protected void ProcessGame(IGame game, string? action = null)
        {
            var user = GetLoggedUser();
            if (user == null)
            {
                return;
            }

            context.Entry(user)
                .Collection(u => u.Games)
                .Load();

            GameEntry? entry = user.Games
                .Where(g => g.Type == "TicTacToe" && g.Active == true)
                .FirstOrDefault();

            if (entry == null)
            {
                entry = new GameEntry
                {
                    Type = game.Type,
                    UserId = user.Id,
                    Active = !game.IsFinished(),
                    Data = game.SaveState()
                };
                context.Games.Add(entry);
                context.SaveChanges();
            }
            else if (entry.Data != null)
            {
                game.LoadState(entry.Data);
            }

            if (action != null)
            {
                var validActions = game.GetValidActions();
                if (validActions.Contains(action))
                {
                    game.Action(action);
                    entry.Active = !game.IsFinished();
                    entry.Data = game.SaveState();
                    context.SaveChanges();
                }
            }
        }
    }
}
