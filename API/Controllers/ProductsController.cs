using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
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
        */
        private readonly IProductRepository _repo;

        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _repo.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {

            return await _repo.GetProductByIdAsync(id);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands(){
            return Ok(await _repo.GetProductsBrandsAsync());
        }
         [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes(){
            return Ok(await _repo.GetProductTypeAsync());
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
    */