using FrontToBackEnd.Data;
using FrontToBackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBackEnd.Areas.Areas.Controllers
{
    [Area("AdminArea")]
    public class CategoryController : Controller
    {

        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {

            List<Category> categories = await _context.Categories.Where(m => m.IsDeleted == false).ToListAsync();
            return View(categories);
        }

        public IActionResult Detail(int Id)
        {
            var category = _context.Categories.FirstOrDefault(m => m.Id == Id);
            return View(category);
            //return Json(new
            //{
            //    categoryName = category.Name,
            //    cetegoryDet=category.Products,
            //    action = "Detail",
            //    Id = id

            //}); 
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool isExist = _context.Categories.Any(m => m.Name.ToLower().Trim() == category.Name.ToLower().Trim());

            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu category artiq movcuddur");
                return View();
            }

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int Id)
        {
            Category category = await _context.Categories.Where(m => !m.IsDeleted && m.Id == Id).FirstOrDefaultAsync();
            if (category == null) return NotFound();


            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id,Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (Id != category.Id) return NotFound();

            Category dbCategory = await _context.Categories.AsNoTracking().Where(m => !m.IsDeleted && m.Id == Id).FirstOrDefaultAsync();

            if(dbCategory.Name.ToLower().Trim() == category.Name.ToLower().Trim())
            {
                return RedirectToAction(nameof(Index));
            }

            bool isExist = _context.Categories.Any(m => m.Name.ToLower().Trim() == category.Name.ToLower().Trim());
            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu category artiq movcuddur");
                return View();
            }

            //dbCategory.Name = category.Name;
            _context.Update(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            Category category = await _context.Categories.Where(m=>m.Id == Id).FirstOrDefaultAsync();

            if (category == null) return NotFound();

            //category.IsDeleted = true;
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}


