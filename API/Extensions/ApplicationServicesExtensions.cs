using System.Linq;
using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{

    /* 
    Bu classi olusturmamizin nedeni startup in daha guzel guzukmesini istiyoruz
     */
    public  static class ApplicationServicesExtensions
    {
   public static IServiceCollection AddApplicationServices(
       this IServiceCollection services)    {
       
       // caching must be singleton ready to avaiable when application starts shared all request coming to API
       services.AddSingleton<IResponseCacheService, ResponseCacheService>();
       
       
           services.AddScoped<ITokenService,TokenService>();
           services.AddScoped<IOrderService, OrderService>();
           services.AddScoped<IPaymentService, PaymentService>();
           services.AddScoped<IUnitOfWork, UnitOfWork>();
              services.AddScoped<IProductRepository, ProductRepository>();
              services.AddScoped<IBasketRepository,BasketRepository>();
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));

            services.Configure<ApiBehaviorOptions>(options => {
options.InvalidModelStateResponseFactory = actionContext => {
    var errors = actionContext.ModelState
    .Where(e => e.Value.Errors.Count > 0)
    .SelectMany(x => x.Value.Errors)
    .Select(x => x.ErrorMessage).ToArray();

    var errorResponse = new ApiValidationErrorResponse{
        Errors = errors
    };
    return new BadRequestObjectResult(errorResponse);
};
});

return services;
       } 
    }
}