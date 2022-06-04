using API.Dtos;
using AutoMapper;
using Core.OrderAggregate;
using Microsoft.Extensions.Configuration;

namespace API.Helpers
{
    
    /*
     *eski hali
     * "pictureUrl": "images/products/boot-redis1.png",
     *
     * yeni hali
     *  "pictureUrl": "https://localhost:5001/images/products/boot-redis1.png",
     * 
     */
    // product url resolverda yaptigimiz gibi url nin basina localhost u koyuyoruz
    // mapping profile sinifinda formember ile beirtiyoruz.
    public class OrderItemUrlResolver : IValueResolver<OrderItem,OrderItemDto,string>
    {
        private readonly IConfiguration _config;

        public OrderItemUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ItemOrdered.PictureUrl))
            {
                return _config["ApiUrl"] + source.ItemOrdered.PictureUrl;
            }

            return null;
        }
    }
}