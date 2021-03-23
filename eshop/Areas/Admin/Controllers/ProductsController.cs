using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eshop.Models;
using eshop.Models.Database;
using Microsoft.AspNetCore.Authorization;
using eshop.Models.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace eshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin) + "," + nameof(Roles.Manager))]
    public class ProductsController : Controller
    {
        IHostingEnvironment Env;
        readonly ILogger<ProductsController> _log;
        readonly EshopDBContext EshopDBContext;

        public ProductsController(EshopDBContext eshopDBContext, IHostingEnvironment env, ILogger<ProductsController> log)
        {
            this.EshopDBContext = eshopDBContext;
            Env = env;
            _log = log;
        }

        public async Task<IActionResult> Select()
        {
            ProductViewModel product = new ProductViewModel();
            product.Product = await EshopDBContext.Products.ToListAsync();
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {

                product.ImageSrc = String.Empty;

                FileUpload fup = new FileUpload(Env.WebRootPath, "Products", "image");
                product.ImageSrc = await fup.FileUploadAsync(product.Image);

                EshopDBContext.Products.Add(product);
                await EshopDBContext.SaveChangesAsync();
                _log.LogInformation("Product Created Name:" + product.Nazev);
                return RedirectToAction(nameof(Select));
            }
            else
            {
                return View(product);
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            Product productItem = EshopDBContext.Products.Where(carI => carI.ID == id).FirstOrDefault();
            if (productItem != null)
            {
                return View(productItem);
            }
            else
                return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            Product productItem = EshopDBContext.Products.Where(carI => carI.ID == product.ID).FirstOrDefault();
            if (productItem != null)
            {
                if (ModelState.IsValid)
                {
                    productItem.Nazev = product.Nazev;
                    productItem.Image = product.Image;
                    productItem.Price = product.Price;
                    productItem.ImageAlt = product.ImageAlt;
                    productItem.DetailProduktu = product.DetailProduktu;

                    FileUpload fup = new FileUpload(Env.WebRootPath, "Products", "image");
                    if (String.IsNullOrWhiteSpace(product.ImageSrc = await fup.FileUploadAsync(product.Image)) == false)
                    {
                        productItem.ImageSrc = product.ImageSrc;
                    }

                    await EshopDBContext.SaveChangesAsync();
                    _log.LogInformation("Product Edited Name:" + product.Nazev);
                    return RedirectToAction(nameof(Select));
                }
                else
                {
                    return View(product);
                }
            }
            else
                return NotFound();
        }

        public async Task<IActionResult> Delete(int id)
        {
            Product productItem = EshopDBContext.Products.Where(carI => carI.ID == id).FirstOrDefault();

            if (productItem != null)
            {
                EshopDBContext.Products.Remove(productItem);
                await EshopDBContext.SaveChangesAsync();
                _log.LogInformation("Product Deleted ID:" + id);
                return RedirectToAction(nameof(Select));
            }
            else { return NotFound(); }
        }

    }
}
