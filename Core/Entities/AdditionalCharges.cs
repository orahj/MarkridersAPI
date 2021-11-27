using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enum;

namespace Core.Entities
{
    public class AdditionalCharges : BaseEntity
    {
        public string Name { get; set; }
        public bool? IsFixed { get; set; }
        public double? Amount { get; set; }
        public double? Rate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}