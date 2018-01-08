using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using TestTask.AppStart;
using Microsoft.Extensions.Configuration;
using TestTask.WebUI.Components.Extensions;
using TestTask.Domain;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace TestTask
{
    public class Startup
    {
        public IServiceCollection _services { get; set; }

        public IConfiguration _configuration { get; set; }


        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            _services = services;

            services.AddMvc();
            services.AddtestTaskServices();
            services.AddAppDbContext(_configuration.GetConnectionString("AppMainConnection"));
            services.AddIdentityContext(_configuration.GetConnectionString("AppMainConnection"));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.GetServicesByRoute("/AllServices", _services);
            app.UseMvc(AppRouteBuilder.UseRouters);
            

        }
    }
}
