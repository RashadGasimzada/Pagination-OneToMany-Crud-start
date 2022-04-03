using FrontToBackEnd.Data;
using FrontToBackEnd.Models;
using FrontToBackEnd.ViewModels;
using FrontToBackEnd.ViewModels.Admin.SliderViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBackEnd.Controllers
{
    
   
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.count = _context.Cards.Count();
            List<Slider> sliders = await _context.Sliders.ToListAsync();
            List<Card> cards = await _context.Cards.ToListAsync();
            List<Category> categories = await _context.Categories.ToListAsync();
            List<CategoryName> categoryNames = await _context.CategoryNames.ToListAsync();
            List<Fruit> fruits = await _context.Fruits.ToListAsync();
            List<Beverage> beverages = await _context.Beverages.ToListAsync();
            List<Poster> posters = await _context.Posters.ToListAsync();
            
            
            HomeVM homeVM = new HomeVM
            {
                Sliders= sliders,
                Cards= cards,
                Categories = categories,
                CategoryNames= categoryNames,
                Fruits= fruits,
                Beverages= beverages,
                Posters = posters
               

            };

            return View(homeVM);
        }
    }
}
