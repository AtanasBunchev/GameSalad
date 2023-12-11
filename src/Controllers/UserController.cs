using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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
            if(!ModelState.IsValid)
                return View(model);

            if (model.Username == null) // warning suppression ¯\_(ツ)_/¯
                throw new ArgumentException("Model is Not Valid");

            var user = context.FindByUsername(model.Username);
            if (user == null)
            {
                ModelState.AddModelError("authError",
                    "This user is not registered yet.");
                return View(model);
            }

            if (user.Password != model.Password)
            {
                ModelState.AddModelError("authError",
                    "Incorrect Password");

                return View(model);
            }

            // TODO set JWT token

            return Redirect("/");
        }

        private JwtSecurityToken GenerateUserToken(User user)
        {
            var claims = new[]
            {
                new Claim("LoggedUserId", user.Id.ToString())
            };

            var signingKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(
                    "d418e5ebaee346698ebbbd375ba1f692"));
            var signingCredentials = new SigningCredentials(
                signingKey,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                null, // issuer
                null, // audience
                claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: signingCredentials
            );

            return token;
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
                throw new ArgumentException("Username is NULL");
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
