using GameSalad.Entities;
using GameSalad.Repositories;
//using GameSalad.ViewModels.Profiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameSalad.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ProfilesController : CustomController
{

    public ProfilesController(UsersDbContext context)
        : base(context)
    {
    }

    public IActionResult Index()
    {
        var user = GetLoggedUser();
        if(user == null)
            return Redirect("/");

        context.Entry(user)
            .Collection(u => u.Games)
            .Load();

        return View("Profile", user);
    }

    public IActionResult Index(int id)
    {
        if(GetLoggedUser() == null)
            return Redirect("/");

        var user = context.GetUserById(id);
        if(user == null)
            return Redirect("/");

        context.Entry(user)
            .Collection(u => u.Games)
            .Load();

        return View("Profile", user);
    }
}
