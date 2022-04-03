
using FrontToBackEnd.Data;
using FrontToBackEnd.Models;
using FrontToBackEnd.Utilities.File;
using FrontToBackEnd.Utilities.Helpers;
using FrontToBackEnd.ViewModels.Admin.SliderViewModels;
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
    #region OneFileUpload
    //if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid)
    //{
    //    return View();
    //}

    //if (!slider.Photo.CheckFileType("image/"))
    //{
    //    ModelState.AddModelError("Photo", "Image type is wrong");
    //    return View();
    //}

    //if (!slider.Photo.CheckFileSize(800))
    //{
    //    ModelState.AddModelError("Photo", "Image size is wrong");
    //    return View();
    //}

    //string fileName = Guid.NewGuid().ToString() + "_" + slider.Photo.FileName;

    ////string testpath = @"C:\Users\hp\Desktop\slider - crud\FrontToBackEnd\wwwroot\assets\image" + fileName;
    //string path = Path.Combine(_env.WebRootPath, "AssetsAdmin/assets/images", fileName);

    //using (FileStream stream = new FileStream(path, FileMode.Create))
    //{
    //    await slider.Photo.CopyToAsync(stream);
    //}

    //slider.Image = fileName;
    //await _context.Sliders.AddAsync(slider);
    //await _context.SaveChangesAsync();

    #endregion
    [Area("AdminArea")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _context.Sliders.AsNoTracking().ToListAsync();
            return View(sliders);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var slider = await GetSliderById(id);
            if (slider is null) return NotFound();
            return View(slider);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderVM sliderVM)
        {
            if (ModelState["Photos"].ValidationState == ModelValidationState.Invalid) return View();

            if (ModelState["Desc"].ValidationState == ModelValidationState.Invalid) return View();

            foreach (var photo in sliderVM.Photos)
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

            foreach (var photo in sliderVM.Photos)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;

                string path = Helper.GetFilePath(_env.WebRootPath, "assets/image", fileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }



                


                Slider slider = new Slider
                {
                    Image = fileName,
                   
                };

                await _context.Sliders.AddAsync(slider);

            }

           
            
               
               
               
            

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            Slider slider = await GetSliderById(Id);

            if (slider == null) return NotFound();

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/image", slider.Image);
            Helper.DeleteFile(path);

            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

       

        public async Task<IActionResult> Edit(int Id)
        {
            Slider slider = await GetSliderById(Id);
            if (slider == null) return NotFound();
            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, Slider slider)
        {
            var dbSlider = await GetSliderById(Id);
            if (dbSlider == null) return NotFound();

            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();

            if (!slider.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View(dbSlider);
            }

            if (!slider.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View(dbSlider);
            }

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/image", dbSlider.Image);

            Helper.DeleteFile(path);


            string fileName = Guid.NewGuid().ToString() + "_" + slider.Photo.FileName;

            string newPath = Helper.GetFilePath(_env.WebRootPath, "assets/image", fileName);

            using (FileStream stream = new FileStream(newPath, FileMode.Create))
            {
                await slider.Photo.CopyToAsync(stream);
            }

            dbSlider.Image = fileName;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task<Slider> GetSliderById(int Id)
        {
            return await _context.Sliders.FindAsync(Id);
        }
    }
}
