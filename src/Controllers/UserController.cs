using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GameSalad.ViewModels.User;
using GameSalad.Repositories;
using GameSalad.Entities;

namespace GameSalad.Controllers;

public class UserController : CustomController
{
    [TempData]
    public string? CreatedUser { get; set; }

    public UserController(UsersDbContext context)
        : base(context)
    {
    }


    public IActionResult Login()
    {
        if (GetLoggedUser() != null)
            return Redirect("/");

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
        if (!ModelState.IsValid)
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

        var token = GenerateUserToken(user);
        SetAuthenticationBearerToken(token);

        return Redirect("/");
    }

    protected virtual void SetAuthenticationBearerToken(JwtSecurityToken token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var encodedToken = tokenHandler.WriteToken(token);
        var header = new AuthenticationHeaderValue("Bearer", encodedToken);
        Response.Headers.Authorization = header.ToString();

        // And because browsers don't care about giving us the Authorization back...
        var authCookieName = "access_token";
        Response.Cookies.Append(
            authCookieName,
            encodedToken,
            new CookieOptions()
            {
                Expires = DateTime.Now.AddMinutes(90),
                HttpOnly = true
            }
        );
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
            "the.issuer", // issuer
            "the.audience", // audience
            claims,
            expires: DateTime.UtcNow.AddMinutes(90),
            signingCredentials: signingCredentials
        );

        return token;
    }


    public IActionResult SignUp()
    {
        if (GetLoggedUser() != null)
            return Redirect("/");

        return View();
    }

    [HttpPost]
    public IActionResult SignUp(SignUpVM model)
    {
        if (!ModelState.IsValid)
            return View(model);

        if (model.Username == null || model.Password == null)
        {
             // warning suppression ¯\_(ツ)_/¯
            throw new ArgumentException("Username is NULL");
        }

        if (context.FindByUsername(model.Username) != null)
        {
            ModelState.AddModelError("Username",
                "*This username is already used.");
            return View(model);
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

    public IActionResult Logout()
    {
        var authCookieName = "access_token";
        Response.Cookies.Delete(authCookieName);
        return RedirectToAction("Login");
    }
}
