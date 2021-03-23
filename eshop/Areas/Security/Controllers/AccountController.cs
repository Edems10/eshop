using eshop.Controllers;
using eshop.Models;
using eshop.Models.ApplicationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Areas.Security.Controllers
{
    [Area("Security")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        ISecurityApplicationServices iSecure;
        readonly ILogger<AccountController> _log;

        public AccountController(ISecurityApplicationServices iSecure, ILogger<AccountController> log)
        {
            this.iSecure = iSecure;
            _log = log;
        }



        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public  async Task<IActionResult> Login(LoginViewModel vm)
        {
            vm.LoginFailed = false;
            if (ModelState.IsValid)
            {
                bool isLogged = await iSecure.Login(vm);
                if (isLogged)
                {
                    _log.LogInformation("USER LOGGED IN:"+vm.Username);
                    return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", String.Empty), new { area = "" });
                }
                else
                {
                    vm.LoginFailed = true;

                }
            }
            return View();
        }


        public IActionResult Logout()
        {
            iSecure.Logout();
            _log.LogInformation("USER LOGGED OUT");
            return RedirectToAction(nameof(Login));
        }


        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            vm.ErrorsDuringRegister = null;
            if (ModelState.IsValid)
            {
                vm.ErrorsDuringRegister = await iSecure.Register(vm, Models.Identity.Roles.Customer);

                if (vm.ErrorsDuringRegister == null)
                {
                    var lVM = new LoginViewModel()
                    {
                        Username = vm.Username,
                        Password = vm.Password,
                        RememberMe = true,
                        LoginFailed = false

                    };
                    _log.LogInformation("USER REGISTERED:"+vm.Username);
                    return await Login(lVM);
                }
            }
            return View(vm);
        }
    }
}
