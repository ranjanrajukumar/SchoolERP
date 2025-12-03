using Microsoft.Extensions.DependencyInjection;
using SchoolERP.Application.Interfaces.Utilities;
using SchoolERP.Application.Services.Utilities;
using SchoolERP.Infrastructure.Repositories.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolERP.Infrastructure.Configurations
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserAccountService, UserAccountService>();
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
