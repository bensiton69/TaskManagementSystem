using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagementSystem.Core;
using TaskManagementSystem.Persistence;
using TaskManagementSystem.Services;

namespace TaskManagementSystem.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddDbContextPool<DataContext>(options =>
                options.UseSqlServer(config.GetConnectionString("TaskManagementSystemDBConnection")));
            return services;
        }
    }
}
