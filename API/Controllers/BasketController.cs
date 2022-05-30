using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository){
            _basketRepository = basketRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id){
            var basket = await _basketRepository.GetBasketAsync(id);

// if basket is null we return basket with id that client generated.
            return Ok(basket ?? new CustomerBasket(id));
        }
         [HttpPost]
         public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket){
             var  updatedBasket = await _basketRepository.UpdateBasketAsync(basket);
             return Ok(updatedBasket);
         }
         [HttpDelete]
         public async Task DeleteBasketAsync(string id){
             await _basketRepository.DeleteBasketAsync(id);
         }
    }
}

/* 
we are not storing anthing in our actual db  here is just a place where customers can leave their baskets 
behind in our memory so if  they come back , they can pick up where they let off.
we weill store basket's id in client side.
if they dont come back we will destroy their basket 
 */