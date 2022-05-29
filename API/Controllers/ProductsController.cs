using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    // [ApiController]
    // [Route("api/[controller]")]
    public class ProductsController : BaseApiController
    {
    

        /* 

Bunu Repository Pattern ile yapacagiz
       // Dependency Injection
       private readonly StoreContext _context;
       public ProductsController(StoreContext context)
       {
           _context = context;
       }
        
        private readonly IProductRepository _repo;

        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
        } */

 public readonly IGenericRepository<Product> _productsRepo;
        public readonly IGenericRepository<ProductBrand> _productBrandRepo;
        public readonly IGenericRepository<ProductType> _productTypeRepo;
        public readonly IMapper _mapper;

        /* 
        constructor a birden fazla parametre ekledik bunu unit of work 
        pattern ile halledecegiz.
         */
        public ProductsController(
            IGenericRepository<Product> productsRepo,
            IGenericRepository<ProductBrand> productBrandRepo,
            IGenericRepository<ProductType> productTypeRepo,
            IMapper mapper)
            {
            _productTypeRepo = productTypeRepo;
            _productBrandRepo = productBrandRepo;
            _productsRepo = productsRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
       [FromQuery] ProductSpecParams productParams
            //Daha clean olmasi icin yeni sinif olusturduk
            // string sort,
            // int? brandId,
            // int? typeId
            )
        {
            // var spec = new ProductsWithTypesAndBrandsSpecification(sort,brandId,typeId);
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
            var countSpec = new ProductWithFiltersForCountSpecification(productParams);
            var totalItems = await _productsRepo.CountAsync(countSpec);
            var products = await _productsRepo.ListAsync(spec);

var data = _mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products);
           // var products = await _productsRepo.ListAllAsync();
return Ok(new Pagination<ProductToReturnDto>(
    productParams.PageIndex,
    productParams.PageSize,
    totalItems,
    data));
        }

        [HttpGet("{id}")]

// hata oldugunda ApiResponse sinifindaki modeli donder
   [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            // isimler anlasilmasi icin uzun verilmeli
          var spec = new ProductsWithTypesAndBrandsSpecification(id);
          var product = await _productsRepo.GetEntityWithSpec(spec);
          // product null
          if(product == null ) return NotFound(new  ApiResponse(404));

return _mapper.Map<Product,ProductToReturnDto>(product);

           // return await _productsRepo.GetByIdAsync(id);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands(){

            return Ok(await _productBrandRepo.ListAllAsync());
        }

         [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes(){
            return Ok(await _productTypeRepo.ListAllAsync());
        }
    }

}

/* 
       [HttpGet("{id}")]
   GetProduct a id vermezsek app hangisini baslatacagini bilmez bunu belirtmemiz gerekiyor
   dotnet 

   Neden Dependency Injection?
   Startup dosyasinda ConfigureServices i context i ekledik fakat 
   o context request in   lifetime  boyunca calisiyor  ayakta olacak 
   request bitince dispose edilecek

ControllerBase  vs Controller
 The Controller class derives from ControllerBase 
 and adds some members that are only needed to support Views.

 Request ti asenkron yapmazsak  method tamamlana kadar birsey yapamayiz

 [ApiController]  un gorevi  controllera request geldiginde validation 
 yapiyor GetProduct da integer gelip gelmedigini kontrol ediyor.

GetProducts calistiginda productType ve productBrand null geliyor 
Bunu 2 cozumu var 
Lazy Load => ef will automatically load any navigation properties 
such as brand and type  whenever we request particular 


parametreyi query string olarak gonderiyoruz fakat controlerda object 
Http get request kullandigimizda bodymiz yok bu controleler icin biraz kafa karistirici
HttGet ile object i birbirine baglayamiyor. asgagidaki gibi hata aliyoruz

{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.13",
    "title": "Unsupported Media Type",
    "status": 415,
    "traceId": "00-58e9d201e84ee24487ce5d11d3691aca-515d9253e83cd64b-00"
}

Duzeltmek icin objecye  [FromQuery] i kullaniyoruz.

    */