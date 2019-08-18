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
            List<Chef> allChefs = dbContext.Chefs
                .Include(c => c.CreatedDishes)
                .ToList();
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
                DateTime dob;
                // string date = newChef.Birthday.ToString("MM/dd/yyyy");
                DateTime.TryParse(newChef.Birthday, out dob);
                DateTime currentDate = DateTime.Now;

                TimeSpan difference = currentDate - dob;

                DateTime age = DateTime.MinValue + difference;

                int years = age.Year - 1;

                if(years >= 18)
                {
                    newChef.Age = years;
                    dbContext.Add(newChef);
                    dbContext.SaveChanges();

                    // to properly update CreatedAt and UpdatedAt
                    newChef.CreatedAt = DateTime.Now;
                    newChef.UpdatedAt = DateTime.Now;
                    dbContext.SaveChanges();

                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("Birthday", "Chef is too young");
                return View("NewChef");
            }
            else
            {
                return View("NewChef");
            }
        }

        [HttpGet("dishes")]
        public IActionResult Dishes()
        {
            List<Dish> allDishes = dbContext.Dishes
            .Include(d => d.Creator)
            .ToList();
            return View("Dishes", allDishes);
        }

        [HttpGet("newdish")]
        public IActionResult NewDish()
        {
            List<Chef> allChefs = dbContext.Chefs.ToList();
            @ViewBag.Chefs = allChefs;   // Added "@" which fixed my problem, deleted it then it still works...???
            return View();
        }

        [HttpPost("createdish")]
        public IActionResult CreateDish(Dish newDish)
        {
            if(ModelState.IsValid)
            {
                int calories = (int)newDish.Calories;
                if(calories > 0)
                {
                    dbContext.Add(newDish);
                    dbContext.SaveChanges();

                    // to properly update CreatedAt and UpdatedAt
                    newDish.CreatedAt = DateTime.Now;
                    newDish.UpdatedAt = DateTime.Now;
                    dbContext.SaveChanges();

                    return RedirectToAction("Dishes");
                }

                ModelState.AddModelError("Calories", "Calories must be greater than 0.");

                return View("NewDish");
                
            }
            else
            {
                return View("NewDish");
            }
        }

    }
}
