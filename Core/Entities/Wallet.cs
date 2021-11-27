using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;

namespace Core.Entities
{
    public class Wallet : BaseEntity
    {
        public Wallet(string appUserId, decimal balance, decimal lastSpend, bool isActive)
        {
            AppUserId = appUserId;
            Balance = balance;
            LastSpend = lastSpend;
            IsActive = isActive;
        }

        [Required]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public decimal Balance { get; set; }
        public decimal LastSpend { get; set; }
        public bool IsActive { get; set; }
    }
}