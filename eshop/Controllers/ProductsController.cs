using eshop.Models;
using eshop.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Controllers
{
    public class ProductsController : Controller
    {

        readonly ILogger<ProductsController> _log;
        readonly EshopDBContext EshopDBContext;
        public ProductsController(EshopDBContext eshopDBContext, ILogger<ProductsController> log)
        {
            this.EshopDBContext = eshopDBContext;
            _log = log;
        }
        [Route("Products/{id:int}")]
        public IActionResult Detail(int id)
        {

            Product p = EshopDBContext.Products.Where(carI => carI.ID == id).FirstOrDefault();

            if (p != null)
            {
                _log.LogInformation("Detail produktu :"+p+"zavolan");
                return View(p);
            }
            else return NotFound();
        }
    }
}
