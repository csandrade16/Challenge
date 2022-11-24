using EmployeeTurim.Api.ModelMappers;
using EmployeeTurim.Domain.Interfaces;
using EmployeeTurim.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EmployeeTurim.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEmployeeTurimMapper, EmployeeTurimMapper>();
            services.ConfigureRepositoryServices(configuration);
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerOptionsConfig>();
            return services;
        }
    }
}
