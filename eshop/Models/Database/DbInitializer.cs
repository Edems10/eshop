using eshop.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Database
{
    public static class DbInitializer
    {
        public  static void Initialize(EshopDBContext dbcontext)
        {
            if(dbcontext.Database.EnsureCreated())
            {
                if(dbcontext.Carousels.Count()==0)
                { 
                IList<Carousel> carousels = CarouselHelper.GenerateCarousel();
                foreach (var item in carousels )
                {
                        dbcontext.Carousels.Add(item);
                }
                    dbcontext.SaveChanges();
                }

                //if (dbcontext.Products.Count() == 0)
                //{
                //    IList<Product> products = ProductHelper.GenerateCarousel();
                //    foreach (var item in products)
                //        dbcontext.Products.Add(item);
                //    dbcontext.SaveChanges();

                //}

                


            }

        }

        public async static void EnsureRoleCreated(IServiceProvider serviceProvider)
        {
            using (var service = serviceProvider.CreateScope())
            {
                RoleManager<Role> roleManager = service.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                string[] roles = Enum.GetNames(typeof(Roles));

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(new Role(role));
                }


            }
        }

        public async static void EnsureAdminCreated(IServiceProvider serviceProvider)
        {
            using (var service = serviceProvider.CreateScope())
            {
                UserManager<User> userManager = service.ServiceProvider.GetRequiredService<UserManager<User>>();
              
                User admin = new User()
                {
                    UserName = "admin",
                    Email = "admin_mitrenga@utb.cz",
                    Name = "Adam",
                    LastName = "Mitrenga",
                    EmailConfirmed = true
                };
                var password = "Qwersasg7541@";
               
                User adminInDatabase = await userManager.FindByNameAsync(admin.UserName);
               
                if(adminInDatabase ==null)
                {
                    IdentityResult iResult = await userManager.CreateAsync(admin, password);

                    if(iResult.Succeeded)
                    { 
                    string[] roles = Enum.GetNames(typeof(Roles));
                    foreach(var role in roles)
                        {
                            await userManager.AddToRoleAsync(admin, role);
                        }
                    }else if(iResult.Errors!=null && iResult.Errors.Count()>0)
                    {
                        foreach(var error in iResult.Errors)
                        {
                            Debug.WriteLine("Eror duriong role Creation: " + error.Code + " -> " + error.Description);
                        }
                    }
                }

                User manager = new User()
                {
                    UserName = "manager",
                    Email = "manager_mitrenga@utb.cz",
                    Name = "Adam",
                    LastName = "Mitrenga",
                    EmailConfirmed = true
                };

                User managerInDatabase = await userManager.FindByNameAsync(manager.UserName);

                if (managerInDatabase == null)
                {
                    IdentityResult iResult = await userManager.CreateAsync(manager, password);

                    if (iResult.Succeeded)
                    {
                        string[] roles = Enum.GetNames(typeof(Roles));
                        foreach (var role in roles)
                        {
                            if(role!=Roles.Admin.ToString())
                            await userManager.AddToRoleAsync(manager, role);
                        }
                    }
                    else if (iResult.Errors != null && iResult.Errors.Count() > 0)
                    {
                        foreach (var error in iResult.Errors)
                        {
                            Debug.WriteLine("Eror duriong role Creation: " + error.Code + " -> " + error.Description);
                        }
                    }
                }

            }
        }
    }


    

    






}
