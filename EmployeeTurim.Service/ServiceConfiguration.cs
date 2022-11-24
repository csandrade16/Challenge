using EmployeeTurim.Domain.Interfaces;
using EmployeeTurim.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EmployeeTurim.Service
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection ConfigureRepositoryServices (this IServiceCollection services, IConfiguration configuration)
        {         
            services.Configure<EmployeeRepositorySettings>(
            configuration.GetSection(nameof(EmployeeRepositorySettings)));

            services.AddSingleton<IEmployeeRepositorySettings>(sp => sp.GetRequiredService<IOptions<EmployeeRepositorySettings>>().Value);

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            return services;
        }
    }
}