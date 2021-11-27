using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.DTOs.Wallet;

namespace Core.Interfaces
{
    public interface IWalletRepository
    {
        Task <Result> CreateWallet(CreateWalletDTO data);
        Task <Result> FundWallet(FundWalletDto data);
        Task<Result> FundPaymentWallet(FundPaymentWalletDto data);
        Task<Result> GetUserWalletTransactions(string Id);
        Task<Result> GetWalletBalance(string id);
        Task<Result> GetWalletTransactionsByDate(WalletTransactionDto data);
    }
}