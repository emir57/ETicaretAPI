using ETicaretAPI.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ETicaretAPI.Persistence.IoC
{
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            ConfigurationManager configurationManager = new ConfigurationManager();
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/ETicaretAPI.API"));
            configurationManager.AddJsonFile("appsettings.json");
            var connectionString = configurationManager.GetConnectionString("MySqlConnection");
            services.AddDbContext<ETicaretAPIDbContext>(options => options.UseMySql(connectionString)); ;

            return services;
        }
    }
}
