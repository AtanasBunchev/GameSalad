using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameSalad.ViewModels.User;
using GameSalad.Repositories;

namespace GameSalad.Controllers
{
    public class UserController : Controller
    {
        private UsersDbContext context;
        public UserController(UsersDbContext context)
        {
            this.context = context;
        }


        public IActionResult Login()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(SignUpVM model)
        {
            return View(model);
        }
    }
}
