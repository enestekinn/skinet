using API.Dtos;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;

namespace API.Helpers
{

    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _config;
        public ProductUrlResolver(IConfiguration config)
        {

            _config = config;
        }

        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
                 //check  string  is empty values 
            if(!string.IsNullOrEmpty(source.PictureUrl)){
                    return _config["ApiUrl"] + source.PictureUrl;
            }
            return null;
        }
    }

}
// Apimizde veri images/products/sb-ts1.png seklinde geliyor  bunun basina https eklicez. string 
// destination url  sonucunda ne olcak yani.
// sonuc         "pictureUrl": "https://localhost:5001/images/products/sb-ts1.png",

