using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eshop.Models;
using eshop.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace eshop.Controllers
{
    public class HomeController : Controller
    {

        readonly ILogger<HomeController> _log;
        readonly EshopDBContext EshopDBContext;

        public HomeController(EshopDBContext eshopDBContext, ILogger<HomeController> log)
        {
            this.EshopDBContext = eshopDBContext;
            _log = log;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new CarouselViewModel();
            _log.LogInformation("Index Called");
            vm.Carousel = await EshopDBContext.Carousels.ToListAsync();
            vm.Products = await EshopDBContext.Products.ToListAsync();
            return View(vm);
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            _log.LogInformation("About Called");
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var featureExceptiopn = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            this._log.LogError("Exception occured: " + featureExceptiopn.Error.ToString() + Environment.NewLine + "Exception Path: " + featureExceptiopn);


            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult ErrorCodeStatus(int? statusCode=null)
        {
            string originalURL = String.Empty;
            var features = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            if (features!=null)
            {
                originalURL = features.OriginalPathBase + features.OriginalPath + features.OriginalQueryString;
            }

            var statCode = statusCode.HasValue ? statusCode.Value : 0;
            this._log.LogWarning("Status Code: " + statCode + "Original URL: "+originalURL);

            if(statCode == 404)
            {

                _404ViewModel vm404 = new _404ViewModel()
                {
                    StatusCode = statCode
                };

                return View(statusCode.ToString(),vm404);
            }


            ErrorCodeStatusViewModel vm = new ErrorCodeStatusViewModel()
            {
                StatusCode = statCode,
                OriginalURL = originalURL
            };

            return View(vm);
        }



    }
}
