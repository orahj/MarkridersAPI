using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationUserStore : UserStore<AppUser, AspNetRole, MarkRiderContext, Guid, AspNetUserClaim, AspNetUserRole, AspNetUserLogin, AspNetUserToken, AspNetRoleClaim>, IUserStore<AppUser>
    {
        public ApplicationUserStore(MarkRiderContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}
