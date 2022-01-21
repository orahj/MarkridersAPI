using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Implementations;
using Infrastructure.Services;
using MarkriderAPI.Controllers.errors;
using MarkriderAPI.Email.Implementations;
using MarkriderAPI.Email.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace MarkriderAPI.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<ISecurityService,SecurityService>();
             services.AddScoped<IDeliveryRepository,DeliveryRepository>();
             services.AddScoped<IRiderGuarantorRepository,RiderGuarantorRepository>();
             services.AddScoped<IRiderRepository, RiderRepository>();
             services.AddScoped<IPaymentRepository, PaymentRepository>();
             services.AddScoped<IWalletRepository,WalletRepository>();
            services.AddScoped<INotification, NotificationRepository>();
             services.AddScoped<IGeneralRepository,GeneralRepository>();
             services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IMailClient, MailClient>();
             services.AddScoped<ITokenService,TokenService>();
            services.AddScoped<IDeliveryDetailsRepository, DeliveryDetailsRepository>();
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            services.Configure<ApiBehaviorOptions>(option =>{
                option.InvalidModelStateResponseFactory = actionContext =>{

                    var errors =actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x =>x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };
                     return new BadRequestObjectResult(errorResponse);
                };
                
            });

            return services;
        }
    }
}