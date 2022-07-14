using ETicaretAPI.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Persistence.Repositories;
using ETicaretAPI.Domain.Identity;
using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Persistence.Services;
using ETicaretAPI.Application.Abstractions.Services.Authentication;

namespace ETicaretAPI.Persistence.IoC
{
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<ETicaretAPIDbContext>(options => options.UseMySql(
                GetConnectionString()
                ));
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                //options.Password
            }).AddEntityFrameworkStores<ETicaretAPIDbContext>();
            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();

            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IFileWriteRepository, FileWriteRepository>();

            services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
            services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();

            services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
            services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IExternalAuthentication, AuthService>();
            services.AddScoped<IInternalAuthentication, AuthService>();


            return services;
        }

        private static string GetConnectionString()
        {
            ConfigurationManager configurationManager = new ConfigurationManager();
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/ETicaretAPI.API"));
            configurationManager.AddJsonFile("appsettings.json");
            return configurationManager.GetConnectionString("MySqlConnection");
        }
    }
}
