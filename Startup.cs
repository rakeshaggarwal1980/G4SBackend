using videoApp.VideoChat.Hubs;
using videoApp.VideoChat.Options;
using videoApp.VideoChat.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace videoApp.VideoChat
{
    public class Startup
    {
        readonly IConfiguration _configuration;
        private const string DefaultCorsPolicyName = "localhost";
        public Startup(IConfiguration configuration) => _configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.Configure<TwilioSettings>(settings =>
            {
                settings.AccountSid = "ACdf18fa6baaa5ce6554b45469beff95ff"; //Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
                settings.ApiSecret = "mPCXgpFDoY4rN46xHwH35ukJsyqS0pv6"; //Environment.GetEnvironmentVariable("TWILIO_API_SECRET");
                settings.ApiKey = "SK24c672067c60c99757607f5a14ece7b0"; //Environment.GetEnvironmentVariable("TWILIO_API_KEY");
                                                                        //   settings.ChatServiceSid = "ISfe08e187504b453d819fff6d4343bf8a";
            })
                    .AddTransient<IVideoService, VideoService>();

            services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .SetIsOriginAllowed((x) => true)
                               .AllowCredentials();
                    });
            });
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //  .UseSpaStaticFiles();


            app.UseRouting();
            app.UseCors(DefaultCorsPolicyName);
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