using System;
using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {

        // when program runs  this method will be executed 
        public static async Task Main(string[] args)
        {
/* 
    dataContext e erismek istiyoruz ama burada services disindayiz 
yani Startup classin disindayiz  bir nesne yarattigimizda o nesne 
uygulama boyunca ayakta kalmaz

 */            
            //CreateHostBuilder(args).Build().Run();
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope()){
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try {
                    var context = services.GetRequiredService<StoreContext>();
                    await context.Database.MigrateAsync();
               // static method seeding data program basladiginda yuklenecek
               await StoreContextSeed.SeedAsync(context,loggerFactory);
                }catch(Exception ex){
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex,"An error occired during migration");
                }
            }
            // run demeyi unutma 
            host.Run();
        }


// apply default settings for out app 
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
