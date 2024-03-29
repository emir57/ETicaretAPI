using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETicaretAPI.API
{
    public class Program
    {
        public static void Main(string[] args)
        {

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    Logger log = new LoggerConfiguration()
                        .WriteTo.Console()
                        .WriteTo.File("logs/log.txt")
                        .WriteTo.MySQL(
                        "Server=localhost;Port=3306;Database=ETicaretAPIDb;Uid=root;Pwd=123456;",
                        "logs")
                        .WriteTo.Seq("http://localhost:5341/")
                        .Enrich.FromLogContext()
                        .CreateLogger();
                    webBuilder.UseSerilog(log);
                    webBuilder.UseStartup<Startup>();
                });
    }
}
