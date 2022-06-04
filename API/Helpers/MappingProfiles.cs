using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.OrderAggregate;
using Address = Core.Entities.Identity.Address;

namespace API.Helpers
{
    
    // Profile nugetpackage dan geliyor.
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product,ProductToReturnDto>()
            .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
            .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
            .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());

            // ReverseMap CreateMap i iki yonlu calistiriyor Address to AddressDto || AddressDto to Address
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<AddressDto, Core.OrderAggregate.Address>();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(
                    d => d.DeliveryMethod,
                    o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippingPrice,
                    o => o.MapFrom(s => s.DeliveryMethod.Price));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId,
                    o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
                .ForMember(d => d.ProductName,
                    o => o.MapFrom(s => s.ItemOrdered.ProductName))
                .ForMember(d => d.PictureUrl,
                    o => o.MapFrom(s => s.ItemOrdered.PictureUrl))
                .ForMember(d => d.PictureUrl,
                    o => o.MapFrom<OrderItemUrlResolver>());

        }
        
        
    }
}
/* 
Product modelini ProductToReturnDto'a ceviriyoruz AutoMapper ile
yaptigimiz bu isi Startup Class'da bildirmemiz gerekiyor.

            services.AddAutoMapper(typeof(MappingProfiles));

            Product ta 
        public ProductType ProductType { get; set; }
ProductToReturnDto da 
 public string ProductType { get; set; }

 Istek attigimizda bize  Core.Entities.ProductType yani string degerini
 donduruyor. Bunu ForMember kullanarak hallediyoruz.


 json verimizde listenin icindeki bazi veriler null geliyordu onlari formember ile cekerek aldik .

 */