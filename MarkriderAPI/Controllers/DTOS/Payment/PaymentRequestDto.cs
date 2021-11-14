using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enum;

namespace MarkriderAPI.Controllers.DTOS.Payment
{
    public class PaymentRequestDto
    {
         public class PayoutListDto
        {
            public Guid Id { get; set; }
            public decimal Amount { get; set; }
            public DateTime Date { get; set; }
            public PayOutStatus PayOutStatus { get; set; }
            public string BusinessName { get; set; }
        }
        public class CreatePayOut
        {
            public decimal Amount { get; set; }
            public string UserId { get; set; }
            public string Id { get; set; }

        }
        public class VerifyAccountNumber
        {
            public bool status { get; set; }
            public string message { get; set; }
            public Data data { get; set; }
        }

        public class CreateAccountTransfer
        {
            public string type { get; set; }
            public string name { get; set; }
            public string account_number { get; set; }
            public string bank_code { get; set; }
            public string currency { get; set; }
        }
        public class TransferObject
        {
            public string source { get; set; }
            public string amount { get; set; }
            public string recipient { get; set; }
            public string reason { get; set; }
        }
        public class TransferObjectResponse
        {
            public bool status { get; set; }
            public string message { get; set; }
            public TransferObjectResponseData data { get; set; }
        }
        public class WeeklyPayout
        {
            public decimal Balance { get; set; }
            public decimal PayoutTotal { get; set; }
            public decimal SalesTotal { get; set; }
        }
        public class TransferObjectResponseData
        {
            public string reference { get; set; }
            public double integration { get; set; }
            public string domain { get; set; }
            public decimal amount { get; set; }
            public string currency { get; set; }
            public string source { get; set; }
            public string reason { get; set; }
            public double recipient { get; set; }
            public string status { get; set; }
            public string transfer_code { get; set; }
            public int id { get; set; }
            public DateTime createdAt { get; set; }
            public DateTime updatedAt { get; set; }
        }
        public class TransferrecipientResponse
        {
            public bool status { get; set; }
            public string message { get; set; }
            public TransaferRecipeintData data { get; set; }
        }
        public class TransaferRecipeintData
        {
            public bool active { get; set; }
            public DateTime createdAt { get; set; }
            public string currency { get; set; }
            public string domain { get; set; }
            public int id { get; set; }
            public int integration { get; set; }
            public string name { get; set; }
            public string recipient_code { get; set; }
            public string type { get; set; }
            public DateTime updatedAt { get; set; }
            public bool is_deleted { get; set; }
            public TransferRecipientDateDetails details { get; set; }
        }
        public class TransferRecipientDateDetails
        {
            public string authorization_code { get; set; }
            public string account_number { get; set; }
            public string account_name { get; set; }
            public string bank_code { get; set; }
            public string bank_name { get; set; }
        }
        public class Data
        {
            public string account_number { get; set; }
            public string account_name { get; set; }
            public int bank_id { get; set; }
        }
        public class BVNVerificationObject
        {
            public string bvn { get; set; }
            public string account_number { get; set; }
            public string bank_code { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string middle_name { get; set; }
        }
        public class BVNVerificationResponse
        {
            public bool status { get; set; }
            public string message { get; set; }
            public BVNVerificationData data { get; set; }
            public BVNVerificationMeta meta { get; set; }

        }
        public class BVNVerificationData
        {
            public string bvn { get; set; }
            public bool is_blacklisted { get; set; }
            public bool account_number { get; set; }
            public bool first_name { get; set; }
            public bool middle_name { get; set; }
            public bool last_name { get; set; }
        }
        public class BVNVerificationMeta
        {
            public int calls_this_month { get; set; }
            public int free_calls_left { get; set; }
        }
    }
}