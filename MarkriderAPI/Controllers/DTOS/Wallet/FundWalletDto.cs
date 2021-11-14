using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkriderAPI.Controllers.DTOS.Wallet
{
    public class FundWalletDto
    {
        public string Email { get; set; }
        public decimal Amount { get; set; }
    }
    public class FundPaymentWalletDto
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public Guid GroupId { get; set; }
        public int? MonthId { get; set; }
        public int? Cycle { get; set; }
    }
}