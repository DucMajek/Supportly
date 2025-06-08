using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Supportly.Infrastructure.Models;

namespace Supportly.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration  configurationManager)
        {
            services.AddDbContext<SupportlyContext>(options => 
                options.UseSqlServer(
                    configurationManager.GetConnectionString("DBConnection"),
                    b => b.MigrationsAssembly(typeof(SupportlyContext).Assembly)
                ));
            return services;
        }
    }
}
