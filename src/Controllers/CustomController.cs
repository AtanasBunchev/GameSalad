using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using GameSalad.Repositories;
using GameSalad.Entities;

namespace GameSalad.Controllers
{
    public class CustomController : Controller
    {
        protected UsersDbContext context;

        public CustomController(UsersDbContext context)
        {
            this.context = context;
        }

        protected virtual User? GetLoggedUser()
        {
            var claim = (HttpContext?.User?.Claims as ClaimsPrincipal)
                ?.FindFirst("LoggedUserId");

            if(!int.TryParse(claim?.Value, out int userId))
                return null;

            return context.GetUserById(userId);
        }
    }
}
