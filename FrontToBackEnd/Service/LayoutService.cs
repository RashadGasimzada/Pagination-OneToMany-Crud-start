using FrontToBackEnd.Data;
using FrontToBackEnd.Models;
using FrontToBackEnd.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBackEnd.Service
{
    public class LayoutService
    {
        private readonly AppDbContext _context;
        public LayoutService(AppDbContext context)
        {

            _context = context;
        }

        
        public  Dictionary<string, string> GetSettings()
        {
            Dictionary<string, string> settings = _context.Settings.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);
            return settings;
        }
        //public async Task<IActionResult> GetDatas()
        //{
        //    List<Category> categories = await _context.Categories.ToListAsync();
        //    List<CategoryName> categoryNames = await _context.CategoryNames.ToListAsync();
        //    List<Fruit> fruits = await _context.Fruits.ToListAsync();
        //    List<Beverage> beverages = await _context.Beverages.ToListAsync();
        //    LayoutVM layoutVM = new LayoutVM
        //    {
        //        Categories=categories,
        //        CategoryNames=categoryNames,
        //        Fruits=fruits,
        //        Beverages =beverages,
        //    };
        //    return (IActionResult)layoutVM;
        //}
        public List<Category> GetCategories()
        {
            List<Category> categories = _context.Categories.ToList();
            return categories;
        }
        public List<CategoryName> GetCategoryNames()
        {
            List<CategoryName> categoryNames = _context.CategoryNames.ToList();
            return categoryNames;
        }
        public List<Fruit> GetFruit()
        {
            List<Fruit> fruits = _context.Fruits.ToList();
            return fruits;
        }
        public List<Beverage> GetBeverage()
        {
            List<Beverage> beverages = _context.Beverages.ToList();
            return beverages;
        }
    }
}
