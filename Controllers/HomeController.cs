using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ChefsNDishes.Models;
using System.Linq;

namespace ChefsNDishes.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            List<Chef> allChefs = dbContext.Chefs.ToList();
            return View("Index", allChefs);
        }

        [HttpGet("newchef")]
        public IActionResult NewChef()
        {
            return View();
        }

        [HttpPost("createchef")]
        public IActionResult CreateChef(Chef newChef)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(newChef);
                dbContext.SaveChanges();

                // to properly update CreatedAt and UpdatedAt
                newChef.CreatedAt = DateTime.Now;
                newChef.UpdatedAt = DateTime.Now;
                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View("NewChef");
            }
        }

    }
}
