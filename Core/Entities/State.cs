using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class State
    {
        public string Id { get; set; }
        public string Name { get; set; }
         public virtual ICollection<AppUser> AppUsers { get; set; }
    }
}