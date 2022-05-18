using ETicaretAPI.Application.Services;
using ETicaretAPI.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaretAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureService(
            this IServiceCollection services)
        {
            services.AddScoped<IFileService, FileService>();
        }
    }
}
