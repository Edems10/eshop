using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using eshop.Models;
using eshop.Models.Database;
using eshop.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;

namespace eshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin) + "," + nameof(Roles.Manager))]
    public class CarouselController : Controller
    {
        readonly ILogger<CarouselController> _log;
        IHostingEnvironment Env;
        readonly EshopDBContext EshopDBContext;


        public CarouselController(EshopDBContext eshopDBContext, IHostingEnvironment env, ILogger<CarouselController> log)
        {
            this.EshopDBContext = eshopDBContext;
            Env = env;
            _log = log;
        }

        public async Task<IActionResult> Select()
        {
            _log.LogInformation("Select Carrousel Called");
            CarouselViewModel carousel = new CarouselViewModel();
            carousel.Carousel = await EshopDBContext.Carousels.ToListAsync();
            return View(carousel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Carousel carousel)
        {
            if (ModelState.IsValid)
            {

                carousel.ImageSrc = String.Empty;

                FileUpload fup = new FileUpload(Env.WebRootPath, "Carousels", "image");
                _log.LogInformation("Carousels File uploaded");
                carousel.ImageSrc = await fup.FileUploadAsync(carousel.Image);

                EshopDBContext.Carousels.Add(carousel);
                _log.LogInformation("NewCarousel Added");
                await EshopDBContext.SaveChangesAsync();
                return RedirectToAction(nameof(Select));
            }
            else
            {
                return View(carousel);
            }
        }

        public IActionResult Create()
        {

            _log.LogInformation("Create View Called");
            return View();
        }

        public IActionResult Edit(int id)
        {
            Carousel carouselItem = EshopDBContext.Carousels.Where(carI => carI.ID == id).FirstOrDefault();
            _log.LogInformation("ID of eddited Carousel item :"+id);
            if (carouselItem != null)
            {
                return View(carouselItem);
            }
            else
                return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Carousel carousel)
        {
            Carousel carouselItem = EshopDBContext.Carousels.Where(carI => carI.ID == carousel.ID).FirstOrDefault();
            if (carouselItem != null)
            {
                if (ModelState.IsValid)
                {
                    carouselItem.DataTarget = carousel.DataTarget;
                    carouselItem.ImageAlt = carousel.ImageAlt;
                    carouselItem.CarouselContent = carousel.CarouselContent;

                    FileUpload fup = new FileUpload(Env.WebRootPath, "Carousels", "image");
                    if (String.IsNullOrWhiteSpace(carousel.ImageSrc = await fup.FileUploadAsync(carousel.Image)) == false)
                    {
                        carouselItem.ImageSrc = carousel.ImageSrc;
                    }
                    
                    await EshopDBContext.SaveChangesAsync();
                    _log.LogInformation("Controller corectly edited");
                    return RedirectToAction(nameof(Select));
                }
                else
                {
                    _log.LogInformation("Carousel Model state was invalid");
                    return View(carousel);
                }
            }
            else
                return NotFound();
        }

        public async Task<IActionResult> Delete(int id)
        {
            Carousel carouselItem = EshopDBContext.Carousels.Where(carI => carI.ID == id).FirstOrDefault();

            if (carouselItem != null)
            {
                
                EshopDBContext.Carousels.Remove(carouselItem);
                await EshopDBContext.SaveChangesAsync();
                _log.LogInformation("Carousel ID:" + id + " was deleted");
                return RedirectToAction(nameof(Select));
                
            }
            else { return NotFound(); }
        }

    }
}
