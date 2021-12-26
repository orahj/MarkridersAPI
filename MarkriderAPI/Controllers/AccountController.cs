using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Core.DTOs;
using Core.DTOs.Wallet;
using Core.Entities.Identity;
using Core.Interfaces;
using MarkriderAPI.Controllers.DTOS;
using MarkriderAPI.Controllers.errors;
using MarkriderAPI.Email.Interfaces;
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
        private readonly IEmailService _emailService;
        private readonly IGeneralRepository _generalRepository;
        

        public AccountController(ILogger<AccountController> logger, UserManager<AppUser> userManager,
         SignInManager<AppUser> signInManager,
          ITokenService tokenService, IConfiguration config,
          IRiderGuarantorRepository riderGuarantorRepository, 
          IEmailService emailService,
          IRiderRepository riderRepository,IWalletRepository walletRepository,IGeneralRepository generalRepository)
        {
            _riderGuarantorRepository = riderGuarantorRepository;
            _config = config;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _riderRepository = riderRepository;
            _walletRepository = walletRepository;
            _generalRepository = generalRepository;
            _emailService = emailService;
        }
        //[Authorize]
        [HttpGet("current-user")]
        public async Task<ActionResult<Result>> GetCurrentUser()
        {
            var user = await _userManager.FindUserByClaimPrincipleWithState(HttpContext.User);
            var usr = new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreatToken(user),
                UserName = user.Email,
                Id = user.Id.ToString(),
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
        [HttpGet("get-states")]
        public async Task<ActionResult<Result>> GetStates()
        {
            var states = await _generalRepository.GetState();
            return new Result
            {
                IsSuccessful = true,
                Message ="States retrieved successfully!",
                ReturnedObject = states
            };
        }
        [HttpGet("get-countries")]
        public async Task<ActionResult<Result>> GetCountries()
        {
            var countries = await _generalRepository.GetCountry();
            return new Result
            {
                IsSuccessful = true,
                Message ="States retrieved successfully!",
                ReturnedObject = countries
            };
        }
        [HttpPost("login")]
        public async Task<ActionResult<Result>> Login(LoginDto loginDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginDto.Email);

                //user not found
                if(user == null) return NotFound(new ApiResponse(404));

                var result = await _signInManager.CheckPasswordSignInAsync(user,loginDto.Password,false);

                //not authorized
                if(!result.Succeeded) return Unauthorized(new ApiResponse(401));
                //get states
                var state = await _generalRepository.GetStateById(user.StateId);

                //get con=untry
                var country = await _generalRepository.GetCountryById(user.CountryId);
                var usr = new UserDto
                {
                    Email = user.Email,
                    Token = _tokenService.CreatToken(user),
                    UserName = user.Email,
                    Id = user.Id.ToString(),
                    UserTypes = user.UserTypes,
                    FirstName = user.FirstName,
                    LastName=user.LastName,
                    Avatar=user.Avatar,
                    State = state.Name,
                    Country = country.Name,
                    PhoneNumber = user.PhoneNumber
                };
                return new Result
                {
                    IsSuccessful = true,
                    Message = "Logged in successfully",
                    ReturnedObject = usr
            };
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

         [HttpPost("register")]
        public async Task<ActionResult<Result>> Register(RegisterDto model)
        {
             var userExist = await _userManager.FindByEmailAsync(model.Email);
            if (userExist != null) return new BadRequestObjectResult(new ApiValidationErrorResponse{Errors = new []
            {
                "Email address already exist!"
            }});
            var url = _config["ApiUrl"] +"images/temp/avatar.jpeg";
            var user = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName  = model.FirstName,
                LastName = model.LastName,
                Address  = model.Address,
                IsActive = model.IsActive,
                UserTypes = model.UserTypes,
                DateRegistered = DateTime.Now,
                StateId = model.State,
                CountryId = model.Country,
                UserCategory = model.UserCategory,
                Avatar = url,
                PhoneNumber = model.Phone
            };
            //check for user type and category
            if(model.UserTypes == Core.Enum.UserTypes.Users)
            {
                if(model.UserCategory == Core.Enum.UserCategory.SME)
                {
                    user.Percentage = 5;
                    user.BusinessName = model.BusinessName;
                    user.BusinessNumber = model.BusinessNumber;
                    user.Gender = Core.Enum.Gender.Male;
                }
                if(model.UserCategory == Core.Enum.UserCategory.Company)
                {
                    user.Percentage = 10;
                    user.CompanyName = model.CompanyName;
                    user.RCNumber = model.RCNumber;
                    user.Gender = Core.Enum.Gender.Male;
                }
                if(model.UserCategory == Core.Enum.UserCategory.Individual)
                {
                    user.Percentage = 1;
                    user.Gender = model.Gender;
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
                var rider = new RiderDTO{AppUserId = user.Id.ToString()};
                var createdRider = await _riderRepository.CreateRiderAsync(rider);
                var riderGuarantor = new RiderGuarantorDTO {RiderId = createdRider.Id};
                var createdRiderGuarator = await _riderGuarantorRepository.CreateRiderGuarantorAsync(riderGuarantor);
            }
            //create wallet
            if(model.UserTypes == Core.Enum.UserTypes.Users)
            {
                var wallet = new CreateWalletDTO(user.Id.ToString(),0,0,true);
                var createdWallet = await _walletRepository.CreateWallet(wallet);
            }
            var usr = new UserDto{Email = user.Email, UserName = user.Email, Token = _tokenService.CreatToken(user), 
            UserTypes = user.UserTypes,FirstName = user.FirstName,LastName=user.LastName,Avatar=user.Avatar};

            //send emails
            var token = WebUtility.UrlEncode(await _userManager.GenerateEmailConfirmationTokenAsync(user));

            var verifyUrl = $"{_config["EmailConfig:VerificationEndPoint"]}?token={token}&Email={user.Email}";
            await _emailService.SendWelcomeMailAsync(user, verifyUrl);

            return new Result
            {
                IsSuccessful = true,
                Message ="Resgistered successfully",
                ReturnedObject = usr
            };
        }

        [HttpPost("send-password-resetLink")]
        [AllowAnonymous]
        public async Task<ActionResult<Result>> SendPasswordResetLink([FromBody] SendPasswordResetDto data)
        {
             var user = await _userManager.FindByEmailAsync(data.Email);
            if (user == null) return NotFound(new ApiResponse(404));
            var token = WebUtility.UrlEncode(await _userManager.GeneratePasswordResetTokenAsync(user));
             var resetPasswordUrl = $"{_config["EmailConfig:ResetPasswordEndPoint"]}?token={token}&Email={data.Email}";

            //call email service
            var browserName = HttpContext.Request.Headers.ContainsKey("User-Agent") ? HttpContext.Request.Headers["User-Agent"].ToString() : null;
            var operatingSystem = Environment.OSVersion.ToString();
            await _emailService.SendPasswordResetMailAsync(user, resetPasswordUrl,"",operatingSystem,browserName);
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
        public async Task<ActionResult<Result>> ResetPassword([FromBody] ResetPasswordResetDto data)
        {
            var user = await _userManager.FindByEmailAsync(data.Email);
            data.Token  = HttpUtility.UrlDecode(data.Token);
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
            await _emailService.SendWelcomeMailAsync(user, verifyUrl);
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
        //[Authorize]
        [HttpPut("update-user-info")]
        public async Task<ActionResult<Result>> UpdateUserInfo([FromBody] UpdateUserdto data)
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
        //[Authorize]
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
         //[Authorize]
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
        private async Task ProcessUpdateUserInfo(UpdateUserdto model, AppUser user)
        {
            if (user.PhoneNumber != model.Phone && model.Phone != null)
            {
                user.PhoneNumber = model.Phone;
                user.PhoneNumberConfirmed = false;
            }

            user.FirstName = model.FirstName ?? user.FirstName;
            user.LastName = model.LastName ?? user.LastName;
            user.Avatar = model.Avatar ?? user.Avatar;
            user.RCNumber = model.RCNumber ?? user.RCNumber;
            user.CompanyName = model.CompanyName ?? user.CompanyName;
            user.BusinessName = model.BusinessName ?? user.BusinessName;
            user.BusinessNumber = model.BusinessNumber ?? user.BusinessNumber;

            var result = await _userManager.UpdateAsync(user);
        }
        [HttpPut("rider-online-status")]
        public async Task<ActionResult<Result>> OnlineStatus([FromBody] RiderStatusDTO data)
        {
            var updateRider = await _riderRepository.ChangeStatus(data);
            if (updateRider)
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
    }
}