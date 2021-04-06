using CloudGenThermometer.ApplicationServices.Concretes;
using CloudGenThermometer.Shared.Abstracts;
using Microsoft.Extensions.DependencyInjection;

namespace CloudGenThermometer.Mediator
{
    public static class ApplicationServicesHelper
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IThermometerServices, ThermometerServices>();

            return services;
        }
    }
}