using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace API
{
    public class Program
    {

        // when program runs  this method will be executed 
        public static void Main(string[] args)
        {
            
            CreateHostBuilder(args).Build().Run();
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
