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
            services.AddControllers().AddNewtonsoftJson(options => 
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddApplicationService();
            services.AddIdentityServices(_config);
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();
             services.AddDbContext<MarkRiderContext>(x => 
                x.UseSqlServer(_config.GetConnectionString("DefaultConnection")));

           services.AddSwaggerDocumentation();
           services.AddCors( opt => {
               opt.AddPolicy("CorsPolicy", policy =>{
                   policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:8100");
               });
           });
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
            });
        }
    }
}
