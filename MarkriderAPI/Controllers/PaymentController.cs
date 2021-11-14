using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarkriderAPI.Controllers.DTOS;
using MarkriderAPI.Controllers.DTOS.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static MarkriderAPI.Controllers.DTOS.Payment.PaymentRequestDto;

namespace MarkriderAPI.Controllers
{
    [Authorize]
    public class PaymentController : BaseAPiController
    {
         //not in use
        [HttpPost("Createplan")]
        [ProducesResponseType(typeof(CreatePaystackSubscriptionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Createplan(CreatePaystackSubscriptionRequest paystackSubscriptionRequest)
        {

            return Ok();
        }



        [HttpPost("verifyTransaction")]
        [ProducesResponseType(typeof(CreatePaystackSubscriptionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> verifyTransactionAsync(VerifyTransaction paystackSubscriptionRequest)
        {
            return Ok();
        }


        [HttpPost("verifyFirstTransaction")]
        [ProducesResponseType(typeof(CreatePaystackSubscriptionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> VerifyFirstTransactionAsync(VerifyTransaction paystackSubscriptionRequest)
        {
            return Ok();
        } [HttpPost("bvn-look-up")]
        [ProducesResponseType(typeof(BVNVerificationObject), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BVNVerificationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BVNLookUp(BVNVerificationObject data)
        {
            return Ok();
        }

        [HttpGet("PaymentCharge")]
        [ProducesResponseType(typeof(ExtraCharges), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPaymentCharge(decimal Amount)
        {
            return Ok();
        }
        [HttpPost("payment-with-transfer")]
        [ProducesResponseType(typeof(FundPaymentTransferDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PaymentWithTransfer(FundPaymentTransferDto data)
        {
            return Ok();
        }
        [HttpPost("validate-transfer")]
        [ProducesResponseType(typeof(ValidateTransfer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAjoGroups(ValidateTransfer data)
        {
            var response = new Result();

            try
            {
                return Ok();
            }
            catch (Exception e)
            {
                response.IsSuccessful = false;
                response.Message = e.Message;
                response.ReturnedCode = StatusCodes.Status500InternalServerError.ToString();

                return BadRequest(response);
            }
    }
}
}