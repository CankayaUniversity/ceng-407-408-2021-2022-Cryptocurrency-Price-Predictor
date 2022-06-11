using Application.Interfaces.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Common.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>),typeof(Repository<>));

            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.MaxDepth = 64;
            });

            return services;
        }
    }
}
