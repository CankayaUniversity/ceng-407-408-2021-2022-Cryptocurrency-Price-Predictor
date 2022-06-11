using Application.Validators.EntityValidator;
using Shared.Validators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Validators
{
    public static class ValidatorConfigration
    {
        public static IServiceCollection AddValidatorConfigrationLayer(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidationFilter));
            }).AddFluentValidation(c =>
            {
                c.RegisterValidatorsFromAssemblyContaining<LoginValidator>();
                c.RegisterValidatorsFromAssemblyContaining<RegisterValidator>();
                c.ImplicitlyValidateChildProperties = true;
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }
    }
}
