using Amazon.SimpleEmail;
using EmailService.Service;
using Microsoft.Extensions.DependencyInjection;
using SendGrid;
using Microsoft.Extensions.Configuration;
using Amazon;

namespace EmailService
{
    public static class DIConfiguration
    {
        public static IServiceCollection AddEmailServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<AwsEmailService>();
            services.AddTransient<SendGridEmailService>();
            services.AddSingleton<IAmazonSimpleEmailService>(sp =>
            {
                var config = configuration.GetSection("AWSSESConfig");
                var region = config["Region"];
                if (string.IsNullOrEmpty(region))
                    throw new Exception($"Invalid AWSSESConfig Region: {region}");

                var regionEndpoint = RegionEndpoint.GetBySystemName(region);
                return new AmazonSimpleEmailServiceClient(regionEndpoint);
            });
            services.AddSingleton<ISendGridClient>(sp =>
            {
                var config = configuration.GetSection("SendGridConfig");
                var apiKey = config["ApiKey"];
                if (string.IsNullOrEmpty(apiKey))
                    throw new Exception($"Invalid SendGridConfig API Key: {apiKey}");

                return new SendGridClient(apiKey);
            });
            services.AddTransient<IEmailService>(sp =>
            {
                var config = configuration.GetSection("EmailConfig");
                var emailProvider = config["Provider"];
                if (emailProvider == "AWSSES")
                {
                    return sp.GetRequiredService<AwsEmailService>();
                }
                else if (emailProvider == "SendGrid")
                {
                    return sp.GetRequiredService<SendGridEmailService>();
                }
                else
                {
                    throw new Exception($"Invalid email provider configured: {emailProvider}");
                }
            });
            return services;
        }
    }
}
