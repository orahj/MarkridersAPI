using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Wallet : BaseEntity
    {
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public decimal Balance { get; set; }
        public decimal LastSpend { get; set; }
        public bool IsActive { get; set; }
    }
}