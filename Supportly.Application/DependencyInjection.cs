using Microsoft.Extensions.DependencyInjection;
using Supportly.Application.Services;

namespace Supportly.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<UserService>();
            services.AddScoped<AuthenticationService>();
            return services;
        }
    }    
}

