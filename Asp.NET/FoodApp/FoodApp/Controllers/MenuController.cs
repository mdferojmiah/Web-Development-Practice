using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FoodApp.Data;
using FoodApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FoodApp.Controllers
{
    public class MenuController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public MenuController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var dishes = await _appDbContext.Dishes.ToListAsync();
            return View(dishes);
        }
        [HttpGet]
        public async Task<IActionResult> Index(string query)
        {
            var dishes = from d in _appDbContext.Dishes select d;
            if(!string.IsNullOrEmpty(query)){
                dishes = dishes.Where(d => d.Name.Contains(query));
            }
            return View(await dishes.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var dish = await _appDbContext.Dishes.
                                Include(d => d.DishIngredients!).
                                ThenInclude(di => di.Ingredient).
                                FirstOrDefaultAsync(d => d.Id == id);
            if (dish == null)
            {
                return NotFound();
            }
            return View(dish);
        }
    }
}