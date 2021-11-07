using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class UserTypeDiscount : BaseEntity
    {
        public int Rate { get; set; }
        public int UserType { get; set; }
    }
}