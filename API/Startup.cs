using API.Extensions;
using API.Helpers;
using API.Middleware;
using AutoMapper;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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


            //refactoring this class 
                    services.AddApplicationServices();

// extension func
            services.AddSwaggerDocumentation();



         
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

            app.UseMiddleware<ExceptionMiddleware>();

                
            
            if (env.IsDevelopment())
            {// burasi sadece developer  modda calisiyor production'da degil
            // asagidaki kod yerine yukaridaki  app.UseMiddleware<ExceptionMiddleware>();
            // kullaniyoruz.
            
              //  app.UseDeveloperExceptionPage();

         
            }

/* 
request geldi fakat bizim oyle bir methodumuz yoksa o request bizim
error controllerimizi calistiracak.
SORU =>
how is "/errors/{0}" going to pass the correct placeholder. 
I do not understand  how 0 is replaced with status code.
CEVAP =>
The {0} is just a placeholder for a number that will be replaced
 with the status code integer automatically during redirect.

 */
app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseRouting();
    //api yimizda olan resmi https://localhost:5001/images/products/sb-ts1.png  dosya
    //yolu ile acamiyoruz o yuzden ekledik.
            app.UseStaticFiles();

            app.UseAuthorization();

// we did this as extension function
            app.UseSwaggerDocumentation();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
