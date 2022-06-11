using Application.Api;
using Shared.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services
{
    public static class HttpClientServicesConfiguration
    {
        public static IServiceCollection AddHttpClientServiceCollection(this IServiceCollection services)
        {
            services.AddHttpClient(Constants.HtppClientSettingsApiServiceName,  x =>
            {
                string servicesApiUrl = ProjectConfiguration.Configuration.GetSection("Services:ApiUrl").Value;
                string apiClientToken = ProjectConfiguration.CurrentUser.Token;
                x.BaseAddress = new Uri(servicesApiUrl);
                x.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiClientToken}");
            }).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });

            return services;
        }
    }
}
