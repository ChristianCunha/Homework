using Homework.Application.Services;

namespace Homework.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<IPurchaseService, PurchaseService>();
            return services;
        }
    }
}
