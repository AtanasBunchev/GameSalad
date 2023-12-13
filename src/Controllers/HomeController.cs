using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameSalad.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
