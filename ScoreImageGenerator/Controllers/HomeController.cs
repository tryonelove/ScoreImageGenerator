using System;
using Microsoft.AspNetCore.Mvc;

namespace ScoreImageGenerator.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        } 
    }
}