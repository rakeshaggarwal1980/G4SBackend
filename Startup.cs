using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using VideoChat.Abstractions;
using VideoChat.Hubs;
using VideoChat.Options;
using VideoChat.Services;

namespace VideoChat
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

            services.AddControllersWithViews();

            services.Configure<TwilioSettings>(
                settings =>
                {
                    settings.AccountSid = "ACdf18fa6baaa5ce6554b45469beff95ff"; //Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
                    settings.ApiSecret = "mPCXgpFDoY4rN46xHwH35ukJsyqS0pv6"; //Environment.GetEnvironmentVariable("TWILIO_API_SECRET");
                    settings.ApiKey = "SK24c672067c60c99757607f5a14ece7b0"; //Environment.GetEnvironmentVariable("TWILIO_API_KEY");
                    settings.ChatServiceSid = "ISfe08e187504b453d819fff6d4343bf8a";
                })
                .AddTransient<IVideoService, VideoService>();
                
            services.AddSignalR();

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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");

                endpoints.MapHub<NotificationHub>("/notificationHub");
            });
        }
    }
}
