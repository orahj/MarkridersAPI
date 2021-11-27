using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;

namespace Core.Specifications
{
    public class AppUserSpec : BaseSpecification<AppUser>
    {
        public AppUserSpec(string  email) : base( x=>x.Email == email){}
    }
}