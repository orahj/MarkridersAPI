using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.DTOs;
using Core.DTOs.Wallet;
using Core.Entities.Identity;
using Core.Interfaces;
using MarkriderAPI.Controllers.DTOS;
using MarkriderAPI.Controllers.errors;
using MarkriderAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MarkriderAPI.Controllers
{
    public class AccountController : BaseAPiController
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _config;
        private readonly IRiderGuarantorRepository _riderGuarantorRepository;
        private readonly IRiderRepository _riderRepository;
        private readonly IWalletRepository _walletRepository;

        public AccountController(ILogger<AccountController> logger, UserManager<AppUser> userManager,
         SignInManager<AppUser> signInManager,
          ITokenService tokenService, IConfiguration config,
          IRiderGuarantorRepository riderGuarantorRepository, 
          IRiderRepository riderRepository,IWalletRepository walletRepository )
        {
            _riderGuarantorRepository = riderGuarantorRepository;
            _config = config;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _riderRepository = riderRepository;
            _walletRepository = walletRepository;
        }
        [Authorize]
        [HttpGet("current-user")]
        public async Task<ActionResult<Result>> GetCurrentUser()
        {
            var user = await _userManager.FindUserByClaimPrincipleWithState(HttpContext.User);
            var usr = new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreatToken(user),
                UserName = user.Email,
                Id = user.Id,
                State = user.State.Name,
                Country = user.Country.Name,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return new Result
            {
                IsSuccessful = true,
                Message ="Retrieved successfully",
                ReturnedObject = usr
            };
        }
        [HttpGet("email-exist")]
        public async Task<ActionResult<Result>> CheckEmailExist([FromQuery] string email)
        {
            var b = await _userManager.FindByEmailAsync(email) != null;
            return new Result
            {
                IsSuccessful = b,
                Message ="User Found"
            };
        }
        [HttpPost("login")]
        public async Task<ActionResult<Result>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if(user == null) return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user,loginDto.Password,false);

            if(!result.Succeeded) return Unauthorized(new ApiResponse(401));
            var usr = new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreatToken(user),
                UserName = user.Email,
                Id = user.Id,
                UserTypes = user.UserTypes
            };
            return new Result
            {
                IsSuccessful = true,
                Message = "Logged in successfully",
                ReturnedObject = usr
            };
        }

         [HttpPost("register")]
        public async Task<ActionResult<Result>> Register(RegisterDto model)
        {
             var userExist = await _userManager.FindByEmailAsync(model.Email);
            if (userExist != null) return new BadRequestObjectResult(new ApiValidationErrorResponse{Errors = new []
            {
                "Email address already exist!"
            }});
            var user = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName  = model.FirstName,
                LastName = model.LastName,
                Address  = model.Address,
                IsActive = model.IsActive,
                UserTypes = model.UserTypes,
                DateRegistered = DateTime.UtcNow,
                Gender = model.Gender,
                StateId = model.State,
                CountryId = model.Country,
                UserCategory = model.UserCategory
            };
            //check for user type and category
            if(model.UserTypes == Core.Enum.UserTypes.Users)
            {
                if(model.UserCategory == Core.Enum.UserCategory.SME)
                {
                    user.Percentage = 5;
                }
                if(model.UserCategory == Core.Enum.UserCategory.Company)
                {
                    user.Percentage = 10;
                }
                if(model.UserCategory == Core.Enum.UserCategory.Individual)
                {
                    user.Percentage = 1;
                }
            }
            var res = await _userManager.CreateAsync(user,model.Password);

            if(!res.Succeeded) return new BadRequestObjectResult( new ApiValidationErrorResponse{Errors = new []
            {
                res.Errors.FirstOrDefault().Description
            }});
            //chech if user is a rider
            if(model.UserTypes == Core.Enum.UserTypes.Riders)
            {
                //create rider 
                var rider = new RiderDTO{AppUserId = user.Id};
                var createdRider = await _riderRepository.CreateRiderAsync(rider);
                var riderGuarantor = new RiderGuarantorDTO {RiderId = createdRider.Id};
                var createdRiderGuarator = await _riderGuarantorRepository.CreateRiderGuarantorAsync(riderGuarantor);
            }
            //create wallet
            if(model.UserTypes == Core.Enum.UserTypes.Users)
            {
                var wallet = new CreateWalletDTO(user.Id,0,0,true);
                var createdWallet = await _walletRepository.CreateWallet(wallet);
            }
            var usr = new UserDto{Email = user.Email, UserName = user.Email, Token = _tokenService.CreatToken(user), UserTypes = user.UserTypes};
            return new Result
            {
                IsSuccessful = true,
                Message ="Resgistered successfully",
                ReturnedObject = usr
            };
        }

        [HttpPost("send-password-resetLink")]
        [AllowAnonymous]
        public async Task<ActionResult<Result>> SendPasswordResetLink([FromBody]PasswordDataDto data)
        {
             var user = await _userManager.FindByEmailAsync(data.Email);
            if (user == null) return NotFound(new ApiResponse(404));
            var token = WebUtility.UrlEncode(await _userManager.GeneratePasswordResetTokenAsync(user));
             var resetPasswordUrl = $"{_config["EmailConfig:ResetPasswordEndPoint"]}?token={token}&Email={data.Email}";

             //call email service

             //pepare object to send 

            var p = new PasswordResetResponseDto{Url = resetPasswordUrl};
            return new Result
            {
                IsSuccessful = true,
                Message = "Reset password link sent to you email",
                ReturnedObject = p
            };
        }

        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<ActionResult<Result>> ResetPassword([FromBody]PasswordDataDto data)
        {
            var user = await _userManager.FindByEmailAsync(data.Email);
            if (user == null) return NotFound(new ApiResponse(404));
             var result = await _userManager.ResetPasswordAsync(user, data.Token, data.NewPassword);
             if(!result.Succeeded) return BadRequest(new ApiResponse(400));
             var res = new Result
             {
                 IsSuccessful = true,
                 Message = "Changed successfully"
             };
             return res;
        }
         [HttpPost("send-Verify-emailLink")]
        [AllowAnonymous]
        public async Task<ActionResult<Result>> SendVerifyEmailLink([FromBody]VerifyMailDto data)
        {
            var user = await _userManager.FindByEmailAsync(data.Email);
            if (user == null) return NotFound(new ApiResponse(404));
             var token = WebUtility.UrlEncode(await _userManager.GenerateEmailConfirmationTokenAsync(user));

            var verifyUrl = $"{_config["EmailConfig:VerificationEndPoint"]}?token={token}&Email={data.Email}";

            //send email
            var url = new EmailVeriyResponseDto{Url = verifyUrl};
            return new Result
            {
                IsSuccessful = true,
                Message = "Verification Link sent to your mail",
                ReturnedObject = url
            };
        }
        [HttpPost("verify-mail")]
        [AllowAnonymous]
        public async Task<ActionResult<Result>> VerifyMail([FromBody]VerifyMailDto data)
        {
             var user = await _userManager.FindByEmailAsync(data.Email);
            if (user == null) return NotFound(new ApiResponse(404));
             data.Token = WebUtility.UrlDecode(data.Token);

            var result = await _userManager.ConfirmEmailAsync(user, data.Token);
            if(!result.Succeeded) return BadRequest(new ApiResponse(400));

            return new Result
            {
                IsSuccessful = true,
                Message = "Email Verified successfully"
            };
        }
        [Authorize]
        [HttpPost("update-user-info")]
        public async Task<ActionResult<Result>> UpdateUserInfo([FromBody]RegisterDto data)
        {
             var user = await _userManager.FindByEmailAsync(data.Email);
            if (user == null) return NotFound(new ApiResponse(404));
            
             await ProcessUpdateUserInfo(data, user);
            return new Result
            {
                IsSuccessful = true,
                Message ="Update successful!"
            };
        }
        [Authorize]
        [HttpPut("update-rider-information")]
        public async Task<ActionResult<Result>> UpdateRider([FromBody] RiderDTO data)
        {
            var updateRider = await _riderRepository.UpdateRiderAsync(data);
            if(updateRider)
            {
                return new Result
                {
                    IsSuccessful = true,
                    Message = "Rider Information updated successfully!"
                };
            }

             return new Result
            {
                IsSuccessful = false,
                Message = "Rider not found!"
            };
           
        }
         [Authorize]
        [HttpPut("update-guarantor-information")]
        public async Task<ActionResult<Result>> UpdateRiderGuarantor([FromBody] RiderGuarantorDTO data)
        {
            var updateRiderGuarantor = await _riderGuarantorRepository.UpdateRiderGuarantorAsync(data);
            if(updateRiderGuarantor)
            {
                return new Result
                {
                    IsSuccessful = true,
                    Message = "Rider Guarantor Information updated successfully!"
                };
            }

             return new Result
            {
                IsSuccessful = false,
                Message = "Error updating rider's guarantor information!"
            };
           
        }
        private async Task ProcessUpdateUserInfo(RegisterDto model, AppUser user)
        {
            if (user.PhoneNumber != model.Phone && model.Phone != null)
            {
                user.PhoneNumber = model.Phone;
                user.PhoneNumberConfirmed = false;
            }

            user.FirstName = model.FirstName ?? user.FirstName;
            user.LastName = model.LastName ?? user.LastName;
            user.Avatar = model.Avatar;

            var result = await _userManager.UpdateAsync(user);
        }
    }
}