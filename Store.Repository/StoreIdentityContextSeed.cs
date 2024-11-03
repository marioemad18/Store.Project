using Microsoft.AspNetCore.Identity;
using Store.Data.Entity.IdentityEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository
{
    public class StoreIdentityContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Mario Emad",
                    Email = "mario@gmail.com",
                    UserName = "MarioEmad",
                    Address = new Address
                    {
                        Firstname = "Mario",
                        Lastname = "Emad",
                        City = "Cairo",
                        State = "Cairo",
                        Street = "5",
                        PostalCode = "1233543", 
                    }
                };
                await userManager.CreateAsync(user,"Password123#");
            }
        }
    }
}
