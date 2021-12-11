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
                const string superUserRole = "superuser";
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
                    StateId = 1,
                    CountryId = 1
                };
                await userManager.CreateAsync(user,"P@$$w0rd");
                await AddDefaultRoleToDefaultUser(userManager, superUserRole, user);
            }
            
        }
        private static async Task AddDefaultRoleToDefaultUser(UserManager<AppUser> userManager, string administratorRole, AppUser user)
        {
            await userManager.AddToRoleAsync(user, administratorRole);
        }
    }
}