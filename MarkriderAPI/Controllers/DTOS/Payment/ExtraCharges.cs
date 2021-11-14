using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkriderAPI.Controllers.DTOS.Payment
{
     public class ExtraCharge
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public int PaymentMethod { get; set; }
    }
    public class ExtraCharges
    {
        public List<ExtraCharge> Extras { get; set; }
        public decimal Total { get; set; }
    }
}