using eshop.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin) + "," + nameof(Roles.Manager) + "," + nameof(Roles.Customer))]
    public class PaypalController : Controller
    {
        public IActionResult Succes()
        {
            ViewData["Message"] = "Confirm Your order Now";
            return View();
        }
        public IActionResult Reject()
        {
            ViewData["Message"] = "There was an issue with your payment";
            return View();
        }
    }
}
