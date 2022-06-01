using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;

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


 */