using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.DTOs.Payment;
using Core.Entities.Identity;
using Core.Interfaces;
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
    //[Authorize]
    public class PaymentController : BaseAPiController
    {
        private readonly IPaymentRepository _payment;
        private readonly UserManager<AppUser> _userManager;
        public PaymentController(IPaymentRepository payment,UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _payment = payment;
        }
        [HttpPost("verifyTransaction")]
        //[ProducesResponseType(typeof(CreatePaystackSubscriptionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> verifyTransactionAsync(VerifyTransaction paystackSubscriptionRequest)
        {
            var userEmail = await _userManager.FindByEmailAsync(paystackSubscriptionRequest.Email);
            //var email = HttpContext.User.RetrieveEmailFromPrincipal();
            if(userEmail == null)
            {
                return NotFound(new ApiResponse(404));
            }
            paystackSubscriptionRequest.UserId = userEmail.Id.ToString();
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
        public async Task<IActionResult> PaymentWithTransfer(FundPaymentTransferDto data)
        {
            //var email = HttpContext.User.RetrieveEmailFromPrincipal();
            var userEmail = await _userManager.FindByEmailAsync(data.Email);
            if (userEmail == null)
            {
                return NotFound(new ApiResponse(404));
            }
            data.UserId = userEmail.Id.ToString();
            var transfer = await _payment.TransferPayment(data);
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