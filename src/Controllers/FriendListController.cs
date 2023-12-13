using GameSalad.Repositories;
using GameSalad.ViewModels.FriendList;
using GameSalad.Entities;
using GameSalad.ExtensionMethods;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameSalad.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class FriendListController : CustomController
{
    [TempData]
    public string? FollowedUser { get; set; }
    public string? UnfollowedUser { get; set; }

    public FriendListController(UsersDbContext context)
        : base(context)
    {
    }

    public IActionResult Index()
    {
        var user = GetLoggedUser();
        if(user == null) // warning suppression ¯\_(ツ)_/¯
            return Redirect("/");

        context.Entry(user)
            .Collection(u => u.Followers)
            .Load();
        context.Entry(user)
            .Collection(u => u.Followed)
            .Load();

        IndexVM model = new IndexVM
        {
            Followers = user.Followers.Select(u => 
                {
                    context.Entry(u)
                        .Reference(u => u.Follower)
                        .Load();
                    return u.Follower;
                }).ToList(),
            Following = user.Followed.Select(u => 
                {
                    context.Entry(u)
                        .Reference(u => u.Target)
                        .Load();
                    return u.Target;
                }).ToList(),
            FollowedUser = FollowedUser,
            UnfollowedUser = UnfollowedUser
        };

        return View(model);
    }

    public IActionResult Follow(string username)
    {
        var self = GetLoggedUser();
        var user = context.FindByUsername(username);
        if(self != null && user != null) {
            if (self != user)
            {
                Follow(self, user);
                FollowedUser = user.Username;
            }
        }
        return RedirectToAction("Index");
    }

    public IActionResult Unfollow(string username)
    {
        var self = GetLoggedUser();
        var user = context.FindByUsername(username);
        if(self != null && user != null) {
            if (self != user)
            {
                Unfollow(self, user);
                UnfollowedUser = user.Username;
            }
        }
        return RedirectToAction("Index");
    }

    protected virtual void Follow(User user1, User user2)
    {
        user1.Follow(user2, context);
    }

    protected virtual void Unfollow(User user1, User user2)
    {
        user1.Unfollow(user2, context);
    }
}
