using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DTOs.Wallet
{
    public class CreateWalletDTO
    {
        public CreateWalletDTO()
        {
        }

        public CreateWalletDTO(string appUserId, decimal balance, decimal lastSpend, bool isActive)
        {
            AppUserId = appUserId;
            Balance = balance;
            LastSpend = lastSpend;
            IsActive = isActive;
        }

        public string AppUserId { get; set; }

        public decimal Balance { get; set; }
        public decimal LastSpend { get; set; }
        public bool IsActive { get; set; }
    }
}