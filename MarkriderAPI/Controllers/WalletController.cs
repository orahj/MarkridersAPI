using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.DTOs.Wallet;
using Core.Entities.Identity;
using Core.Interfaces;
using MarkriderAPI.Controllers.DTOS;
using MarkriderAPI.Controllers.errors;
using MarkriderAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MarkriderAPI.Controllers
{
    //[Authorize]
    public class WalletController : BaseAPiController
    {
        private readonly IWalletRepository _walletRepository;
        private readonly UserManager<AppUser> _userManager;
        public WalletController(IWalletRepository walletRepository,UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _walletRepository = walletRepository;
        }

        [HttpPost("FundWallet")]
        public async Task<IActionResult> FundWallet(FundWalletDto data)
        {
            //var email = HttpContext.User.RetrieveEmailFromPrincipal();
            var userEmail = await _userManager.FindByEmailAsync(data.Email);
            if (userEmail == null)
            {
                return NotFound(new ApiResponse(404));
            }
            if(userEmail.UserTypes != Core.Enum.UserTypes.Users)
            {
                return NotFound(new ApiResponse(404));
            }
            data.UserId = userEmail.Id.ToString();
            var fundWallet = await _walletRepository.FundWallet(data);
           return Ok(fundWallet);
        }
        [HttpPost("make-payment-with-wallet")]
        public async Task<IActionResult> FundPaymentWallet(FundPaymentWalletDto data)
        {
            //var email = HttpContext.User.RetrieveEmailFromPrincipal();
            var userEmail = await _userManager.FindByEmailAsync(data.Email);
            if (userEmail == null)
            {
                return NotFound(new ApiResponse(404));
            }
            data.UserId = userEmail.Id.ToString();
            var payment = await _walletRepository.FundPaymentWallet(data);
           return Ok(payment);
        }

        [HttpGet("GetUserWalletTransactions")]
        //[ProducesResponseType(typeof(WalletTransactionResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserWalletTransactions([FromQuery] string email)
        {
            //var userEmail = HttpContext.User.RetrieveEmailFromPrincipal();
            var userEmail = await _userManager.FindByEmailAsync(email);
            if (userEmail == null)
            {
                return NotFound(new ApiResponse(404));
            }
            var appUser = await _userManager.FindByEmailAsync(userEmail.Email);
            if(appUser == null) return NotFound(new ApiResponse(404));
            var walletTransaction = await _walletRepository.GetUserWalletTransactions(appUser.Id.ToString());
            return Ok(walletTransaction);
        }

        [HttpGet("GetWalletBalance")]
        public async Task<IActionResult> GetWalletBalance([FromQuery] string email)
        {
            //var userEmail = HttpContext.User.RetrieveEmailFromPrincipal();
            var userEmail = await _userManager.FindByEmailAsync(email);
            if (userEmail == null)
            {
                return NotFound(new ApiResponse(404));
            }
            var walletBalance = await _walletRepository.GetWalletBalance(userEmail.Id.ToString());
            return Ok(walletBalance);
        }

        // [HttpGet("GetWalletTransactionsByDate")]
        // public async Task<IActionResult> GetWalletTransactionsByDate(WalletTransactionDto walletTransactionDto)
        // {
        //     return Ok();
        // }

    }
}