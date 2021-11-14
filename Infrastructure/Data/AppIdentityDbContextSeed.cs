using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Core.Enum;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data
{
    public class AppIdentityDbContextSeed
    {
         public static async Task SeedUser(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user =  new AppUser
                {
                    UserName ="test@markrider@gmail.com",
                    Email = "test@markrider@gmail.com",
                    FirstName  = "Test",
                    LastName  ="Test",
                    Address  = "Test ",
                    Avatar  = "Tets",
                    IsActive  = true,
                    UserTypes = UserTypes.Users,
                    DateRegistered = DateTime.UtcNow,
                    Gender  = Gender.Male,
                    StateId = "AB",
                    CountryId = 1
                };
                await userManager.CreateAsync(user,"P@$$w0rd");
            
            }
            
        }
    }
}