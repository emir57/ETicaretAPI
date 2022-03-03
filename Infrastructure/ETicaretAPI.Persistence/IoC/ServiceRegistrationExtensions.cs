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
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,IConfiguration configuration)
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            
            //configurationBuilder.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/ETicaretAPI.API"));
            //configurationBuilder.AddJsonFile("appsettings.json");

            var connectionString = ConfigurationExtensions.GetConnectionString(configuration, "MySqlConnection");
            services.AddDbContext<ETicaretAPIDbContext>(options => options.UseMySql(connectionString)); ;

            return services;
        }
    }
}
