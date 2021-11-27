using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.DTOs.Payment;
using Core.Entities.Identity;
using Infrastructure.Data.Implementations;
using MarkriderAPI.Controllers.DTOS;
using MarkriderAPI.Controllers.errors;
using MarkriderAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Core.DTOs.Payment.PaymentRequestDto;

namespace MarkriderAPI.Controllers
{
    [Authorize]
    public class PaymentController : BaseAPiController
    {
        private readonly PaymentRepository _payment;
        private readonly UserManager<AppUser> _userManager;
        public PaymentController(PaymentRepository payment,UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _payment = payment;
        }
        [HttpPost("verifyTransaction")]
        //[ProducesResponseType(typeof(CreatePaystackSubscriptionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> verifyTransactionAsync(VerifyTransaction paystackSubscriptionRequest)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();
            if(email != paystackSubscriptionRequest.Email)
            {
                return NotFound(new ApiResponse(404));
            }
            var appUser = await _userManager.FindByEmailAsync(paystackSubscriptionRequest.Email);
            paystackSubscriptionRequest.UserId = appUser.Id;
            var payment = await _payment.VerifyPaystackTransFirstTime(paystackSubscriptionRequest);
            return Ok(payment);
        }
        [HttpPost("bvn-look-up")]
        [ProducesResponseType(typeof(BVNVerificationObject), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(BVNVerificationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> BVNLookUp(BVNVerificationObject data)
        {
            var bnvLookUp = await _payment.VerifyBvn(data);
            return Ok(bnvLookUp);
        }
        [HttpPost("payment-with-transfer")]
        [ProducesResponseType(typeof(FundPaymentTransferDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PaymentWithTransfer(FundPaymentTransferDto data)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();
            if(email != data.Email)
            {
                return NotFound(new ApiResponse(404));
            }
            var appUser = await _userManager.FindByEmailAsync(data.Email);
            data.UserId = appUser.Id;
            var transfer = _payment.TransferPayment(data);
            return Ok(transfer);
        }
        [HttpGet("validate-transfer/{Id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> TransferPayment(int Id)
        {
            var completeTransfer = await _payment.CompleteTransferPayment(Id);
            return Ok(completeTransfer);
        }

        [HttpGet("getbanks")]
        //[ProducesResponseType(typeof(BankListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetBank()
        {
            var bankLint = await _payment.BankList();
            return Ok(bankLint);
        }
}
}