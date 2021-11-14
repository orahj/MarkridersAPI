using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkriderAPI.Controllers.DTOS.Payment
{
     public class CreatePaystackSubscriptionRequest
    {
        public CreatePaystackSubscriptionRequest()
        {

            send_invoices = true;
            send_sms = true;
            currency = "NGN";
            amount = amount * 100;
        }
        public string name { get; set; }
        public string description { get; set; }
        public int amount { get; set; }
        public string interval { get; set; }
        public bool send_invoices { get; set; }
        public bool send_sms { get; set; }
        public string currency { get; set; }
        public string MyUserID { get; set; }
    }

    public class CreatePaystackSubscriptionResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }
    public class PaymentRequestCompletion 
    {
        public string Id { get; set; }
        public string Email { get; set; }
    }

    public class LiquidationRequestDTO
    {
        public string UserId { get; set; }
        public string EntityId { get; set; }
        public decimal Amount { get; set; }
    }
    public class Data
    {
        public Data()
        {
            IsSubscribedBYUser = false;
        }
        public long Id { get; set; }
        public string UserID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int amount { get; set; }
        public string interval { get; set; }
        public bool send_invoices { get; set; }
        public bool send_sms { get; set; }
        public string currency { get; set; }
        public int integration { get; set; }
        public string domain { get; set; }
        public string plan_code { get; set; }
        public int invoice_limit { get; set; }
        public bool hosted_page { get; set; }
        public bool migrate { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public bool IsSubscribedBYUser { get; set; }
    }
}