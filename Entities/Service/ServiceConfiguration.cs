using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTurim.Service.Service
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection ConfigureRepositoryServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmployeeRepositorySettings>(
            configuration.GetSection(nameof(EmployeeRepositorySettings)));

            services.AddSingleton<IEmployeeRepositorySettings>(sp => sp.GetRequiredService<IOptions<EmployeeRepositorySettings>>().Value);

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            return services;
        }
    }
}
