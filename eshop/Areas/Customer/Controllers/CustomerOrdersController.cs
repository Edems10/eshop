﻿using eshop.Models;
using eshop.Models.ApplicationServices;
using eshop.Models.Database;
using eshop.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = nameof(Roles.Customer))]
    public class CustomerOrdersController : Controller
    {
        readonly ILogger<CustomerOrdersController> _log;
        ISecurityApplicationServices iSecure;
        EshopDBContext EshopDBContext;

        public CustomerOrdersController(ISecurityApplicationServices iSecure, EshopDBContext eshopDBContext, ILogger<CustomerOrdersController> log)
        {
            this.iSecure = iSecure;
            EshopDBContext = eshopDBContext;
            _log = log;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                User currentUser = await iSecure.GetCurrentUser(User);
                if (currentUser != null)
                {
                    IList<Order> userOrders = await this.EshopDBContext.Orders
                                                                        .Where(or => or.UserId == currentUser.Id)
                                                                        .Include(o => o.User)
                                                                        .Include(o => o.OrderItems)
                                                                        .ThenInclude(oi => oi.Product)
                                                                        .ToListAsync();
                    _log.LogInformation("User requested his orders:"+currentUser.Name);
                    return View(userOrders);
                }
            }

            return NotFound();
        }
    }
}
