using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApp_Investigate.Intefaces;
using WebApp_Investigate.Services;

namespace WebApp_Investigate
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            //1 Inteface with 2 classes
            AddOneIntefaceTwoClass(services);
            //1 class with 2 intefaces
            AddOneClassTwoInteface(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        /// <summary>
        /// 1 inteface with 2 classes
        /// </summary>
        /// <param name="services"></param>
        private void AddOneIntefaceTwoClass(IServiceCollection services)
        {
            services.AddTransient<ICalculate, CalculateLineService>();
            services.AddTransient<ICalculate, CalculateTotalAmountService>();
        }
        /// <summary>
        /// 1 class with 2 intefaces
        /// </summary>
        /// <param name="services"></param>
        private void AddOneClassTwoInteface(IServiceCollection services)
        {
            //services.AddScoped<IReader, OneClassTwoInterfacesService>();
            //services.AddScoped<IHelper, OneClassTwoInterfacesService>();
            services.AddTransient<IReader, OneClassTwoInterfacesService>();
            services.AddTransient<IHelper, OneClassTwoInterfacesService>();
        }
    }
}
