using Application.Interfaces.Business;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Common.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
        {
            services.AddScoped<IAuthBusiness, AuthBusiness>();
            services.AddScoped<IModelPredictionBusiness, ModelPredictionBusiness>();

            return services;
        }
    }
}
