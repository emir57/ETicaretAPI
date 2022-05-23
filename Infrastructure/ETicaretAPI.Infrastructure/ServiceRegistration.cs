using ETicaretAPI.Application.Abstractions.Storage;
using ETicaretAPI.Application.Services;
using ETicaretAPI.Infrastructure.Services;
using ETicaretAPI.Infrastructure.Services.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaretAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(
            this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
        }
    }
}
