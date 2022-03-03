using ETicaretAPI.Presentation.Contexts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.IoC
{
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<ETicaretAPIDbContext>(options=>options.Use)
            //services.AddSingleton<,>();

            return services;
        }
    }
}
