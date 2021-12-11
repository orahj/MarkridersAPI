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
    public class ApplicationRoleStore : RoleStore<AspNetRole, MarkRiderContext, Guid, AspNetUserRole, AspNetRoleClaim>, IRoleStore<AspNetRole>
    {
        public ApplicationRoleStore(MarkRiderContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}
