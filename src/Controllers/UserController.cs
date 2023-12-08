using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GameSalad.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Login()
        {
            ViewData["title"] = "GameSalad | Login";
            return View();
        }

        public IActionResult Register()
        {
            ViewData["title"] = "GameSalad | Register";
            return View();
        }
    }
}
