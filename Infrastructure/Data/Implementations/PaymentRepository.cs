using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Core.DTOs;
using Core.DTOs.Payment;
using Core.Entities;
using Core.Entities.Identity;
using Core.Enum;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using static Core.DTOs.Payment.PaymentRequestDto;

namespace Infrastructure.Data.Implementations
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        private readonly ISecurityService _security;
        private readonly UserManager<AppUser> _userManager;
        public PaymentRepository(IUnitOfWork unitOfWork,IConfiguration config,ISecurityService security, UserManager<AppUser> userManager)
        {
            _security = security;
            _config = config;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<Result> BankList()
        {
             Result res = new Result();
            try
            {
                string url = "bank";
                BankListResponse rt = new BankListResponse();

                var client = new RestClient("https://api.paystack.co/");
                var request = new RestRequest(url, Method.GET);
                request.AddParameter("application/json; charset=utf-8", ParameterType.QueryString);
                request.AddHeader("Authorization", "Bearer " + _config["PaystackKey:TestKey"]);

                request.RequestFormat = DataFormat.Json;
                var response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var resd = JsonConvert.DeserializeObject<BankListResponse>(response.Content);
                    res.IsSuccessful = true;
                    res.Message = "Banks retrived successfully!";
                    res.ReturnedObject = resd;
                    return res;
                }
                else
                {
                    res.IsSuccessful = false;
                    res.Message = "Error Occured!";
                    return res;
                }
            }
            catch (WebException ex)
            {
                res.Message = "Error Occured!";
            }
            return res;
        }

        public async Task<Result> CompleteTransferPayment(int Id)
        {
            var payment = await _unitOfWork.Repository<Payment>().GetByIdAsync(Id);
            if(payment == null) return new Result{IsSuccessful = false, Message = "Payment does not exist!"};
           
            payment.Paid = true;

            _unitOfWork.Repository<Payment>().Update(payment);

            //save changes to context
            var result = await _unitOfWork.Complete();
            return new Result{IsSuccessful = true, Message="Transfer completed successfully!"};
        }

        public async Task<ExtraCharges> GetPaymentCharges(decimal Amount)
        {
            Result res = new Result();
            ExtraCharges myExtras = new ExtraCharges();

            try
            {
                // All Payment Gateways
                int gateways = 1;
                var spec = new AdditionalChargeWithSpec(gateways);
                AdditionalCharges charges = await _unitOfWork.Repository<AdditionalCharges>().GetEntityWithSpec(spec);
                var extraCharges = new ExtraCharge()
                {
                    ID = charges.Id.ToString(),
                    Name = charges.Name,
                    Amount = GetAmount(charges, Amount)
                };
                myExtras.Extras = new System.Collections.Generic.List<ExtraCharge>();
                myExtras.Extras.Add(extraCharges);
                myExtras.Total = Amount + extraCharges.Amount;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return myExtras;
        }

        public async Task<Result> TransferPayment(FundPaymentTransferDto request)
        {
            Result res = new Result();

            var invoiceNo = _security.GetCode("inv").ToUpper();
            var slnNo = _security.GetCode("sln").ToUpper();
            Payment payment = new Payment(request.UserId,Core.Enum.PaymentMethod.Transfer,slnNo,invoiceNo,false,request.TransactionRef,request.TransactionId);
            _unitOfWork.Repository<Payment>().Add(payment);

            //save changes to context
            var result = await _unitOfWork.Complete();
            res.Message = "Transfer initiated successfully, complete the transfer by entering the transaction reference number "+ request.TransactionRef + " in your payment description.";
            res.IsSuccessful = true;
            //notification
            var notification = new Notification
            {
                AppUserId = request.UserId,
                DateCreated = DateTime.Now,
                Read = false,
                Type = NotificationType.WalletUpdate,
                Data = new Dictionary<string, string>
                {
                    { "Title", $"Delivery Payment:Transfer. Amount: {request.amount}" },
                    { "Body", $"You just made payment with transfer; {request.amount}, on {DateTime.Now}." },
                    { "WalletId", $"{payment.Id}"}
                }
            };
            _unitOfWork.Repository<Notification>().Add(notification);
            await _unitOfWork.Complete();
            return res;
        }

        public async Task<Result> VerifyBvn(PaymentRequestDto.BVNVerificationObject obj)
        {
             Result res = new Result();
            if (String.IsNullOrEmpty(obj.bvn))
                return new Result { IsSuccessful = false, Message = "BVN Cannot be empty!" };

            if (String.IsNullOrEmpty(obj.account_number))
                return new Result { IsSuccessful = false, Message = "Account Number Cannot be empty!" };

            if (String.IsNullOrEmpty(obj.bank_code))
                return new Result { IsSuccessful = false, Message = "Bank Code Cannot be empty!" };

            if (String.IsNullOrEmpty(obj.first_name))
                return new Result { IsSuccessful = false, Message = "First Name Cannot be empty!" };

            if (String.IsNullOrEmpty(obj.last_name))
                return new Result { IsSuccessful = false, Message = "BVN Cannot be empty!" };

            string url = "bvn/match";
            var client = new RestClient("https://api.paystack.co/");
            var json = JsonConvert.SerializeObject(obj);
            var request = new RestRequest(url, Method.POST);
            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
            request.AddHeader("Authorization", "Bearer " + _config["PaystackKey:TestKey"]);

            request.RequestFormat = DataFormat.Json;
            var response = client.Execute(request);
            var resd = new BVNVerificationResponse();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                resd = JsonConvert.DeserializeObject<BVNVerificationResponse>(response.Content);
                if (resd.status)
                {
                    return new Result { IsSuccessful = true, Message = resd.message, ReturnedObject = resd.data };
                }

            }
            return new Result { IsSuccessful = false, Message = resd.message, ReturnedObject = resd.data };
        }

        public Task<Result> VerifyPaystackTrans(VerifyTransaction verifyTransaction)
        {
            throw new NotImplementedException();
        }

        public Task<Result> VerifyPaystackTransFirst(VerifyTransaction verifyTransaction)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> VerifyPaystackTransFirstTime(VerifyTransaction verifyTransaction)
        {
             Result res = new Result();
             var pstk = VerifyPaystack(verifyTransaction.TransactionRef);
            if (pstk.data == null)
            {
                res.Message = $"Could not verify Transaction:{verifyTransaction.TransactionRef}";
            }
            else
            {
                 if (pstk.data.status == "success")
                 {
                      var _amt = Convert.ToDecimal(pstk.data.amount);
                      if (_amt == verifyTransaction.amount)
                      {
                        if (pstk.data.amount > 1000)
                        {
                            pstk.data.amount = pstk.data.amount / 100;
                        }
                        var invoiceNo = _security.GetCode("inv").ToUpper();
                        var slnNo = _security.GetCode("sln").ToUpper();

                         Payment payment = new Payment(verifyTransaction.UserId,Core.Enum.PaymentMethod.Paystack,slnNo,invoiceNo,true,verifyTransaction.TransactionRef,verifyTransaction.TransactionId);
                         _unitOfWork.Repository<Payment>().Add(payment);

                        //save changes to context
                        var result = await _unitOfWork.Complete();
                        res.IsSuccessful = true;
                        res.Message = "Transaction verified successfuly!";
                        //notification
                        var notification = new Notification
                        {
                            AppUserId = verifyTransaction.UserId,
                            DateCreated = DateTime.Now,
                            Read = false,
                            Type = NotificationType.WalletUpdate,
                            Data = new Dictionary<string, string>
                            {
                                { "Title", $"Delivery Payment:Paystack. Amount: {pstk.data.amount}" },
                                { "Body", $"You just made payment with paystack; {pstk.data.amount}, on {DateTime.Now}." },
                                { "WalletId", $"{payment.Id}"}
                            }
                        };
                        _unitOfWork.Repository<Notification>().Add(notification);
                        await _unitOfWork.Complete();
                    }
                      else
                      {
                           res.IsSuccessful = false;
                            res.Message = "Amount verification mismatch";
                      }
                 }
                 else
                    {
                        res.IsSuccessful = false;
                        res.Message = pstk.status.ToString();
                    }

                    res.ReturnedObject = pstk.data;
            }
            return res;
        }
         public PSV VerifyPaystack(string transactionref)
         {

            string url = "transaction/verify/" + transactionref;
            PSV rt = new PSV();

            var client = new RestClient("https://api.paystack.co/");
            var request = new RestRequest(url, Method.GET);
            request.AddParameter("application/json; charset=utf-8", ParameterType.QueryString);
            request.AddHeader("Authorization", "Bearer " + _config["PaystackKey:TestKey"]);

            request.RequestFormat = DataFormat.Json;
            var response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var resd = JsonConvert.DeserializeObject<PSV>(response.Content);
                var d = "d";
                return resd;
            }
            else
            {
                return rt;
            }
         }
        private decimal GetAmount(AdditionalCharges AC, decimal Amount)
        {
            decimal charge = 0.0M;

            if (Convert.ToBoolean(!AC.IsFixed))
            {
                decimal percent = Convert.ToDecimal(AC.Rate / (double)100);
                if (AC.PaymentMethod == Core.Enum.PaymentMethod.Paystack)
                {
                    charge = Math.Round(Amount * percent, 2);
                    if (charge > (decimal)AC.Amount)
                        charge = Convert.ToDecimal(AC.Amount);
                }
                else
                {
                    percent = 1 - percent;

                    charge = Math.Round(Amount / percent, 2);
                    charge = charge - Amount;
                    if (charge > (decimal)AC.Amount)
                        charge = Convert.ToDecimal(AC.Amount);
                }
            }

            return charge;
        }

    }
}