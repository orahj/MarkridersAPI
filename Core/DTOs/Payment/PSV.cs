using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DTOs.Payment
{
     public class PSV
    {
        public bool status { get; set; }
        public string message { get; set; }
        public PSVData data { get; set; }
    }
    public class PSVData
    {
        public int id { get; set; }
        public string domain { get; set; }
        public string status { get; set; }
        public string reference { get; set; }
        public int amount { get; set; }
        public object message { get; set; }
        public string gateway_response { get; set; }
        public DateTime paid_at { get; set; }
        public DateTime created_at { get; set; }
        public string channel { get; set; }
        public string currency { get; set; }
        public object ip_address { get; set; }
        public Metadata metadata { get; set; }
        public object log { get; set; }
        public int fees { get; set; }
        public object fees_split { get; set; }
        public Authorization authorization { get; set; }
        public Customer customer { get; set; }
        public string plan { get; set; }
        public object order_id { get; set; }
        public DateTime paidAt { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime transaction_date { get; set; }
        public Plan_Object plan_object { get; set; }
        public Subaccount subaccount { get; set; }
    }

    public class Metadata
    {
        public string invoice_action { get; set; }
    }
    public class BankListResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public ICollection<Datas> data { get; set; }
    }
    public class Datas
    {
        public string name { get; set; }
        public string slug { get; set; }
        public string code { get; set; }
        public string longcode { get; set; }
    }
    public class Authorization
    {
        public string authorization_code { get; set; }
        public string bin { get; set; }
        public string last4 { get; set; }
        public string exp_month { get; set; }
        public string exp_year { get; set; }
        public string channel { get; set; }
        public string card_type { get; set; }
        public string bank { get; set; }
        public string country_code { get; set; }
        public string brand { get; set; }
        public bool reusable { get; set; }
        public string signature { get; set; }
    }

    public class Customer
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string customer_code { get; set; }
        public string phone { get; set; }
        public object metadata { get; set; }
        public string risk_action { get; set; }
    }

    public class Plan_Object
    {
        public int id { get; set; }
        public string name { get; set; }
        public string plan_code { get; set; }
        public string description { get; set; }
        public int amount { get; set; }
        public string interval { get; set; }
        public bool send_invoices { get; set; }
        public bool send_sms { get; set; }
        public string currency { get; set; }
    }

    public class Subaccount
    {
    }
}