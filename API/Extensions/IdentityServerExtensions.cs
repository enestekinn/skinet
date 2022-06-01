using System.Text;
using Core.Entities.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    // 
    public  static class IdentityServerExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,IConfiguration config)
        {
            var builder = services.AddIdentityCore<AppUser>();
            builder = new IdentityBuilder(builder.UserType, builder.Services);
            builder.AddEntityFrameworkStores<AppIdentityDbContext>();
            builder.AddSignInManager<SignInManager<AppUser>>();

            // bunu simdilik koyduk signin managera erismek icin 
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer( options => {
                options.TokenValidationParameters = new TokenValidationParameters{
                    // we might as well just leave anonymous authentication on and a user can send up any old token they want  because we would never validate that 
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
                    ValidIssuer = config["Token:Issuer"],
                    ValidateIssuer = true,
                    /*  
           we didn't do was anthing to do with audience and a token can have an issuer who issued the token and it can also have an audience who the token was issued to 
           what we can also check to see is and say that whilst in this case we're only going to  accept tokens issued by this server ,
           what we can also do is say , we are only going to accept tokens from this particular audience and our audience would be our client app.
           the domain that' being hosted on         
                     */
                    ValidateAudience= false
                };
            });
            return services;
        }
    }
}