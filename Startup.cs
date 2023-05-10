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
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Diagnostics;
using System.IO;

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

            //Cache
            //https://github.com/dotnet/AspNetCore.Docs/blob/main/aspnetcore/performance/caching/distributed/samples/6.x/DistCacheSample/Program.cs
            //from Microsoft.Extensions.Caching.StackExchangeRedis
            //services.AddStackExchangeRedisCache(options =>
            //{
            //    options.Configuration = builder.Configuration.GetConnectionString("MyRedisConStr");
            //    options.InstanceName = "SampleInstance";
            //});
            var host = "localhost";//"172.0.0.1";
            var post = "6379";
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = $"{host}:{post}";
                //options.InstanceName = "SampleInstance";
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IHostApplicationLifetime lifetime)//, IDistributedCache cache)
        {
            ////app.UseSeriLog();
            ////Config for distribute cache.
            //lifetime.ApplicationStarted.Register(() =>
            //{
            //    var currentTimeUTC = DateTime.UtcNow.ToString();
            //    byte[] encodedCurrentTimeUTC = Encoding.UTF8.GetBytes(currentTimeUTC);
            //    var options = new DistributedCacheEntryOptions()
            //        .SetSlidingExpiration(TimeSpan.FromSeconds(20));
            //    cache.Set("cachedTimeUTC", encodedCurrentTimeUTC, options);

            //});

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.

                #region "Custom Exception"

                ////app.UseExceptionHandler("/error");
                //app.UseExceptionHandler(errorApp =>
                //{
                //    errorApp.Run(async context =>
                //    {
                //        context.Response.StatusCode = 500;
                //        context.Response.ContentType = "text/html";

                //        await context.Response.WriteAsync("<html lang=\"en\"><body>\r\n");
                //        await context.Response.WriteAsync("ERROR!<br><br>\r\n");

                //        var exceptionHandlerPathFeature =
                //            context.Features.Get<IExceptionHandlerPathFeature>();

                //        if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
                //        {
                //            await context.Response.WriteAsync(
                //                                      "File error thrown!<br><br>\r\n");
                //        }

                //        await context.Response.WriteAsync(
                //                                      "<a href=\"/\">Home</a><br>\r\n");
                //        await context.Response.WriteAsync("</body></html>\r\n");
                //        await context.Response.WriteAsync(new string(' ', 512));
                //    });
                //});

                #endregion "Custom Exception"
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
