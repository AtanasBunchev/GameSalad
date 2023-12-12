using GameSalad.Entities;
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

        public IActionResult TicTacToe()
        {
            return View();
        }

        public IActionResult TwentyFortyEight()
        {
            return View();
        }
    }
}
