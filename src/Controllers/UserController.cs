using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameSalad.ViewModels.User;
using GameSalad.Repositories;
using GameSalad.Entities;

namespace GameSalad.Controllers
{
    public class UserController : Controller
    {
        private UsersDbContext context;

        [TempData]
        public string? CreatedUser { get; set; }

        public UserController(UsersDbContext context)
        {
            this.context = context;
        }


        public IActionResult Login()
        {
            if (this.CreatedUser == null)
                return View();

            LoginVM model = new LoginVM
            {
                Username = CreatedUser
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Login(LoginVM model)
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
            if (!ModelState.IsValid)
                return View(model);

            if (model.Username != null) // warning suppression ¯\_(ツ)_/¯
            {
                if (context.FindByUsername(model.Username) != null)
                {
                    ModelState.AddModelError("Username",
                        "*This username is already used.");
                    return View(model);
                }
            }
            else
            {
                throw new ArgumentException("Username is Null");
            }

            User user = new User
            {
                Username = model.Username,
                Password = model.Password
            };
            context.Add(user);
            context.SaveChanges();
            this.CreatedUser = user.Username;

            return RedirectToAction("Login");
        }
    }
}
