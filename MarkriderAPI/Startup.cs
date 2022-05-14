using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MarkriderAPI.Helpers;
using MarkriderAPI.MIddleware;
using MarkriderAPI.Controllers.errors;
using MarkriderAPI.Extensions;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Core.DTOs.EmailDto;

namespace MarkriderAPI
{
    public class Startup
    {
          private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
             _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(opt => {
                opt.AddPolicy("CorsPolicy", policy => {
                    policy.AllowAnyHeader().AllowAnyMethod()
                    .WithOrigins("http://localhost:8100", "http://localhost:4200", "http://localhost");
                });
            });
            services.AddControllers().AddNewtonsoftJson(options => 
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddApplicationService();
            services.AddIdentityServices(_config);
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();
            services.Configure<EmailConfig>(_config.GetSection("EmailConfig"));
            services.Configure<EmailRecipient>(_config.GetSection("EmailRecipient"));
            services.AddDbContext<MarkRiderContext>(options => 
            {
                options.UseNpgsql(_config.GetConnectionString("DefaultConnection"));
            });
            services.AddIdentity<AppUser, AspNetRole>(options =>
            {
                options.User.RequireUniqueEmail = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Lockout.MaxFailedAccessAttempts = 4;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            })
            .AddEntityFrameworkStores<MarkRiderContext>()
            .AddDefaultTokenProviders()
            .AddRoleStore<ApplicationRoleStore>()
            .AddUserStore<ApplicationUserStore>();

            services.AddSwaggerDocumentation();
          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleWare>();
        
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwaggerDomcumentation();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToController("Index", "Fallback");
            });
        }
    }
}
