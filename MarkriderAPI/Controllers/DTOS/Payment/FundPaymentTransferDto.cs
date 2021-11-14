using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkriderAPI.Controllers.DTOS.Payment
{
     public class FundPaymentTransferDto
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public Guid GroupId { get; set; }
        public int? MonthId { get; set; }
        public int? Cycle { get; set; }
    }
    public class FundPaymentTransferResponseDto
    {
        public decimal Amount { get; set; }
        public string TransactionRef { get; set; }
    }
    public class ValidateTransfer
    {
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        public int? Cycle { get; set; }
    }
}