using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.DTOs.Payment;
using static Core.DTOs.Payment.PaymentRequestDto;

namespace Core.Interfaces
{
    public interface IPaymentRepository
    {
        Task<Result> VerifyPaystackTrans(VerifyTransaction verifyTransaction);
        Task<Result> VerifyPaystackTransFirstTime(VerifyTransaction verifyTransaction);
        Task<Result> VerifyPaystackTransFirst(VerifyTransaction verifyTransaction);
        Task<Result> BankList();
        Task<Result> VerifyBvn(BVNVerificationObject obj);
        Task <ExtraCharges> GetPaymentCharges(decimal Amount);
        Task<Result> TransferPayment(FundPaymentTransferDto request);
        Task<Result> CompleteTransferPayment(int Id);
        PSV VerifyPaystack(string transactionref);
    }
}