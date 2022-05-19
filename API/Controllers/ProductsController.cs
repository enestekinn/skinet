using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
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
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();
            var products = await _productsRepo.ListAsync(spec);

           // var products = await _productsRepo.ListAllAsync();
return Ok(_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            // isimler anlasilmasi icin uzun verilmeli
          var spec = new ProductsWithTypesAndBrandsSpecification(id);
          var product = await _productsRepo.GetEntityWithSpec(spec);

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

    */