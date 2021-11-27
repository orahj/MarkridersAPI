using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.DTOs.Wallet;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data.Implementations
{
    public class WalletRepository : IWalletRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        private readonly ISecurityService _security;
        private readonly IPaymentRepository _paymentRepository;
        public WalletRepository(IUnitOfWork unitOfWork,IConfiguration config,ISecurityService security,
        IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
            _security = security;
            _config = config;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> CreateWallet(CreateWalletDTO data)
        {
            if(data == null) return new Result{IsSuccessful = false};
            var wallet = new Wallet(data.AppUserId,data.Balance,data.LastSpend,data.IsActive);
             _unitOfWork.Repository<Wallet>().Add(wallet);

            //save changes to context
            var result = await _unitOfWork.Complete();
            return new Result{IsSuccessful = true};
        }

        public async Task<Result> FundPaymentWallet(FundPaymentWalletDto data)
        {
            Result res = new Result();
            //get wallet balance
             var spec = new WalletSpec(data.UserId);
            var wallet = await _unitOfWork.Repository<Wallet>().GetEntityWithSpec(spec);
            if(wallet == null)
            {
                return new Result{IsSuccessful = false, Message = "No Wallet Found!"};
            }
            if(wallet.Balance < data.amount){
                 return new Result{IsSuccessful = false, Message = "No enough fund to perform transaction, please fund your wallet!"};
            }
            var invoiceNo = _security.GetCode("inv").ToUpper();
            var slnNo = _security.GetCode("sln").ToUpper();
            Payment payment = new Payment(data.UserId,Core.Enum.PaymentMethod.Wallet,slnNo,invoiceNo,false,data.TransactionRef,data.TransactionId);
            _unitOfWork.Repository<Payment>().Add(payment);
            
            //update wallet amount
            var walletBalance = wallet.Balance - data.amount;
            wallet.Balance = walletBalance;
            wallet.LastSpend = data.amount;
            _unitOfWork.Repository<Wallet>().Update(wallet);

            //Create wallet tranaction
            WalletTransaction transaction = new WalletTransaction(wallet.Id,Core.Enum.TransactionType.FundRequest,data.amount,
            "Withdrwal",data.TransactionRef,Core.Enum.WalletTransactionStatus.Successful);
            _unitOfWork.Repository<WalletTransaction>().Add(transaction);

            //save changes to context
            var result = await _unitOfWork.Complete();
            res.Message = "Payment successful!";
            res.IsSuccessful = true;
            return res;
        }

        public async Task<Result> FundWallet(FundWalletDto data)
        {
            Result res = new Result();
            //get wallet
            var spec = new WalletSpec(data.UserId);
            var wallet = await _unitOfWork.Repository<Wallet>().GetEntityWithSpec(spec);
            if(wallet == null)
            {
                return new Result{IsSuccessful = false, Message = "No Wallet Found!"};
            }
            //verifu transaction on paystack
            var paystack = _paymentRepository.VerifyPaystack(data.TransactionRef);
            if (paystack.data == null)
            {
                res.Message = $"Could not verify Transaction:{data.TransactionRef}";
            }
            else
            {
                if (paystack.data.status == "success")
                {
                    var _amt = Convert.ToDecimal(paystack.data.amount);
                      if (_amt == data.Amount)
                      {
                          if (paystack.data.amount > 1000)
                        {
                            paystack.data.amount = paystack.data.amount / 100;
                        }
                        //save to wallet transactins
                        WalletTransaction transaction = new WalletTransaction(wallet.Id,Core.Enum.TransactionType.WalletTopUp,data.Amount,
                        "Wallet Top UP",data.TransactionRef,Core.Enum.WalletTransactionStatus.Successful);
                        _unitOfWork.Repository<WalletTransaction>().Add(transaction);
                        //save changes to context
                        var result = await _unitOfWork.Complete();
                      }else
                      {
                           res.IsSuccessful = false;
                            res.Message = "Amount verification mismatch";
                      }
                }else
                {
                    res.IsSuccessful = false;
                        res.Message = paystack.status.ToString();
                }
            }
            return res;
        }

        public async Task<Result> GetUserWalletTransactions(string Id)
        {
             //get wallet
            var spec = new WalletSpec(Id);
            var wallet = await _unitOfWork.Repository<Wallet>().GetEntityWithSpec(spec);
            if(wallet == null) return new Result{IsSuccessful = false, Message="Wallet does not exist!"};
            var walletTranSpec = new WalletTransactionSpec(wallet.Id);
            var walletTransactions = await _unitOfWork.Repository<WalletTransaction>().ListAsync(walletTranSpec);
            var result = new Result
            {
                IsSuccessful = true,
                Message = walletTransactions == null ? "No wallet transaction record found" : null,
                ReturnedObject = walletTransactions
            };

            return result;
        }

        public async Task<Result> GetWalletBalance(string id)
        {
            //get wallet
            var spec = new WalletSpec(id);
            var wallet = await _unitOfWork.Repository<Wallet>().GetEntityWithSpec(spec);
            if(wallet == null)
            {
                return new Result{IsSuccessful = false, Message = "No Wallet Found!"};
            }
            return new Result{IsSuccessful = true, ReturnedObject = wallet};
        }

        public Task<Result> GetWalletTransactionsByDate(WalletTransactionDto data)
        {
            throw new NotImplementedException();
        }
    }
}