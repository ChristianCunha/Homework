using FluentValidation;
using Homework.Domain;
using Homework.Domain.Factory;
using Homework.Domain.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Homework.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {

            services.AddScoped<IPurchaseFactory, PurchaseFactory>();
            services.AddScoped<IValidator<CalculatePurchase>, CalculatePurchaseValidator>();
            return services;
        }
    }
}
