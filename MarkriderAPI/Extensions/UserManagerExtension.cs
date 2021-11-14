using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MarkriderAPI.Extensions
{
    public static class UserManagerExtension
    {
        public  static async Task<AppUser> FindUserByClaimPrincipleWithState(this UserManager<AppUser> input, ClaimsPrincipal user)
        {
            var email = user?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            return await input.Users.Include(e => e.State).Include(x => x.Country).SingleOrDefaultAsync(x => x.Email == email);
        }

        public static async Task<AppUser> FindEmailFromClaimsPricipal(this UserManager<AppUser> input, ClaimsPrincipal user)
        {
            var email = user?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
             return await input.Users.SingleOrDefaultAsync(x => x.Email == email);
        }
    }
}