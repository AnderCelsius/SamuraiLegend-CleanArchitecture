using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SamuraiLegend.Application.Interfaces;
using SamuraiLegend.Domain.Settings;
using SamuraiLegend.Infrastructure.Shared.Services;
using System;

namespace SamuraiLegend.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var emailConfiguratioin = new MailSettings
            {
                Mail = configuration["MailSettings:Mail"],
                DisplayName = configuration["MailSettings:DisplayName"],
                Password = configuration["MailSettings:Password"],
                Host = configuration["MailSettings:Host"],
                Port = Convert.ToInt32(configuration["MailSettings:Port"])
            };

            #region
            services.AddSingleton(emailConfiguratioin);
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddTransient<IEmailService, EmailService>();
            #endregion
        }
    }
}
