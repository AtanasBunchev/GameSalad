using GameSalad.Repositories;
using GameSalad.ViewModels.FriendList;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameSalad.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FriendListController : CustomController
    {
        public FriendListController(UsersDbContext context)
            : base(context)
        {
        }

        public IActionResult Index()
        {
            var user = GetLoggedUser();

            context.Entry(user)
                .Collection(u => u.Followers)
                .Load();
            context.Entry(user)
                .Collection(u => u.Followed)
                .Load();

            IndexVM model = new IndexVM
            {
                Followers = user.Followers.Select(u => u.Follower).ToList(),
                Following = user.Followed.Select(u => u.Target).ToList()
            };

            return View(model);
        }
    }
}
