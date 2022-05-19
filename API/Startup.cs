
using API.Helpers;
using AutoMapper;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {

        // Microsoft  un default olarak verdigi constructorin alternatifi

        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;

        }


        /*      
           public Startup(IConfiguration configuration)
                {
                    Configuration = configuration;
                }

                public IConfiguration Configuration { get; }

                 */

        // This method gets called by the runtime. Use this method to add services to the container.
        // any services we add to our app  we can inject into other classes inside our app
        // di instantiate a class and disposing of it when no longer needed
        //ConfigureServices methodlarin sirasi onemli degil
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddControllers();

            // db context lifetime of request 
            services.AddDbContext<StoreContext>(
                x => x.UseSqlite(_config.GetConnectionString(
                    "DefaultConnection")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIv5", Version = "v1" });
            });
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            services.AddAutoMapper(typeof(MappingProfiles));
            /* 
            Repository sinifi controller a inject edilecek yani her yeni
            request de yeni bir repository class i olusacak
            Burada iki methodumuz var 
AddTransient => instantied for an individual method not request itself
Repository will be created and destroyed just upon an individual method.
Bu cok kisa surer.
AddSingleton =>  Ayni mantik AddTransient ile fakat bu uygulama kapanana kadar
instance i ayakta kalir. Bu cok kisa 
Biz genellikle services.AddScoped();   kullanicaz.
we don't need to worry about disposing of the resources created when
a request comes in.

Generictype i service bu sekilde tanimliyoruz.
  services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));



             */

            //services.AddTransient();
            // services.AddSingleton();
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // Configure methodlarin sirasi onemli.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPIv5 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
    //api yimizda olan resmi https://localhost:5001/images/products/sb-ts1.png  dosya
    //yolu ile acamiyoruz o yuzden ekledik.
            app.UseStaticFiles();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
