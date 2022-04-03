using FrontToBackEnd.Data;
using FrontToBackEnd.Models;
using FrontToBackEnd.Utilities.File;
using FrontToBackEnd.Utilities.Helpers;
using FrontToBackEnd.ViewModels.Admin.PosterViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBackEnd.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class PosterController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public PosterController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Poster> posters = await _context.Posters.ToListAsync();
            return View(posters);
        }

        public IActionResult Detail(int Id)
        {
            var poster = _context.Posters.FirstOrDefault(m => m.Id == Id);
            return View(poster);
            
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PosterVM posterVM)
        {

            if (ModelState["Images"].ValidationState == ModelValidationState.Invalid) return View();

            if (ModelState["Discount"].ValidationState == ModelValidationState.Invalid) return View();

            if (ModelState["Cookie"].ValidationState == ModelValidationState.Invalid) return View();

            if (ModelState["WeekCount"].ValidationState == ModelValidationState.Invalid) return View();

            foreach (var photo in posterVM.Images)
            {
                if (!photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "Image type is wrong");
                    return View();
                }

                if (!photo.CheckFileSize(800))
                {
                    ModelState.AddModelError("Photo", "Image size is wrong");
                    return View();
                }

            }

            foreach (var photo in posterVM.Images)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                
                
                string path = Helper.GetFilePath(_env.WebRootPath, "assets/img", fileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }






                Poster poster = new Poster
                {
                    Image = fileName,
                    Discount = posterVM.Discount,
                    Cookie= posterVM.Cookie,
                    WeekCount = posterVM.WeekCount

                };

                await _context.Posters.AddAsync(poster);

            }








            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int Id)
        {
            Poster poster = await GetPosterById(Id);
            if (poster == null) return NotFound();
            return View(poster);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, Poster poster)
        {
            var dbPoster = await GetPosterById(Id);
            if (dbPoster == null) return NotFound();

            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();

            if (!poster.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View(dbPoster);
            }

            if (!poster.Photo.CheckFileSize(800))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View(dbPoster);
            }

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img", dbPoster.Image);

            Helper.DeleteFile(path);


            string fileName = Guid.NewGuid().ToString() + "_" + poster.Photo.FileName;

            string newPath = Helper.GetFilePath(_env.WebRootPath, "assets/img", fileName);

            using (FileStream stream = new FileStream(newPath, FileMode.Create))
            {
                await poster.Photo.CopyToAsync(stream);
            }

            dbPoster.Image = fileName;
            dbPoster.Discount = poster.Discount;
            dbPoster.Cookie = poster.Cookie;
            dbPoster.WeekCount = poster.WeekCount;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            Poster poster =await _context.Posters.Where(m => m.Id == Id).FirstOrDefaultAsync();

            if (poster == null) return NotFound();

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img", poster.Image);
            Helper.DeleteFile(path);

            _context.Posters.Remove(poster);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<Poster> GetPosterById(int Id)
        {
            return await _context.Posters.FindAsync(Id);
        }
    }
}
