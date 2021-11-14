using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarkriderAPI.Controllers.DTOS;
using MarkriderAPI.Controllers.DTOS.Wallet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarkriderAPI.Controllers
{
    [Authorize]
    public class WalletController : BaseAPiController
    {
         [HttpPost("FundWallet")]
        public async Task<ActionResult> FundWallet(FundWalletDto fundWalletDto)
        {
           return Ok();
        }
        [HttpPost("make-payment-with-wallet")]
        public async Task<IActionResult> FundPaymentWallet(FundPaymentWalletDto fundWalletDto)
        {
           return Ok();
        }

        [HttpGet("GetUserWalletTransactions")]
        [ProducesResponseType(typeof(WalletTransactionResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserWalletTransactions([FromQuery] string email)
        {
            return Ok();
        }

        [HttpGet("GetWalletBalance")]
        public async Task<IActionResult> GetWalletBalance([FromQuery] string email)
        {
            return Ok();
        }

        [HttpGet("GetWalletTransactionsByDate")]
        public async Task<IActionResult> GetWalletTransactionsByDate(WalletTransactionDto walletTransactionDto)
        {
            return Ok();
        }

    }
}