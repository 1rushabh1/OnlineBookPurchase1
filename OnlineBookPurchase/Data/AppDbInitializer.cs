using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OnlineBookPurchase.Data.Static;
using OnlineBookPurchase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookPurchase.Data
{
    public class AppDbInitializer
    {
        //public static void Seed(IApplicationBuilder applicationBuilder)
        //{
        //    using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        //    {
        //        var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

        //        context.Database.EnsureCreated();

        //        // Publications 

        //        if (!context.Publications.Any())
        //        {
        //            context.Publications.AddRange(new List<Publications>()
        //            {
        //                new Publications()
        //                {
        //                    FullName = "Publication 1",
        //                    ProfilePictureURL = "https://images-platform.99static.com//abPQlKQ-HYrja-fsEnmdiHhtxdU=/192x177:806x791/fit-in/590x590/projects-files/99/9928/992803/f433518e-c15a-4940-ba9d-1c4ad5e0cc5a.jpg ",
        //                    Bio = "This Is Bio Of Publication 1"
        //                },

        //                new Publications()
        //                {
        //                    FullName = "Publication 2",
        //                    ProfilePictureURL = "https://images-platform.99static.com//abPQlKQ-HYrja-fsEnmdiHhtxdU=/192x177:806x791/fit-in/590x590/projects-files/99/9928/992803/f433518e-c15a-4940-ba9d-1c4ad5e0cc5a.jpg ",
        //                    Bio = "This Is Bio Of Publication 2"
        //                },

        //            });
        //            context.SaveChanges();
        //        }

        //        // Writer

        //        if (!context.Writers.Any())
        //        {
        //            context.Writers.AddRange(new List<Writer>()
        //            {
        //                new Writer()
        //                {
        //                    FullName = "Writer 1",
        //                    ProfilePictureURL = "data:image/jpeg;base64,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",
        //                    Bio = "This Is Bio Of Writer 1"
        //                },

        //                new Writer()
        //                {
        //                    FullName = "Writer 2",
        //                    ProfilePictureURL = "data:image/jpeg;base64,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",
        //                    Bio = "This Is Bio Of Writer 2"
        //                },

        //            });
        //            context.SaveChanges();
        //        }



        //        // Book

        //        if (!context.Books.Any())
        //        {
        //            context.Books.AddRange(new List<Book>()
        //            {
        //                new Book()
        //                {
        //                    Name = "Book 1",
        //                    ImageURL = "https://rukminim2.flixcart.com/image/416/416/kq2o2vk0/book/q/t/5/sapiens-a-brief-history-of-humankind-original-imag45btntcwmzyp.jpeg?q=70",
        //                    Description = "This Is Description Of First Book 1",
        //                    Price = "$100",
        //                    BookCategory = BookCategory.Autobiography,
        //                    PublicationId = 3
        //                },

        //                 new Book()
        //                {
        //                    Name = "Book 2",
        //                    ImageURL = "https://rukminim2.flixcart.com/image/416/416/kq2o2vk0/book/q/t/5/sapiens-a-brief-history-of-humankind-original-imag45btntcwmzyp.jpeg?q=70",
        //                    Description = "This Is Description Of First Book 2",
        //                    Price = "$100",
        //                    BookCategory = BookCategory.Autobiography,
        //                    PublicationId = 4
        //                },
        //            });
        //            context.SaveChanges();
        //        }

        //        // Book_Writer

        //        if (!context.Book_Writers.Any())
        //        {
        //            context.Book_Writers.AddRange(new List<Book_Writer>()
        //            {
        //                new Book_Writer()
        //                {
        //                    BookId = 8,
        //                    WriterId = 3
        //                },

        //                 new Book_Writer()
        //                {
        //                    BookId = 9,
        //                    WriterId = 4
        //                },
        //            });
        //            context.SaveChanges();
        //        }

        //        // Publications_Writer

        //        if (!context.Publications_Writers.Any())
        //        {
        //            context.Publications_Writers.AddRange(new List<Publications_Writers>()
        //            {
        //                new Publications_Writers()
        //                {
        //                    PublicationsId = 3,
        //                    WriterId = 4
        //                },

        //                 new Publications_Writers()
        //                {
        //                    PublicationsId = 4,
        //                    WriterId = 3
        //                },
        //            });
        //            context.SaveChanges();
        //        }

        //    }
        //}


        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();


                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@onlinebookpurchase.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }



                string appUserEmail = "user@onlinebookpurchase.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Application User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }

            }
        }
    }
}
