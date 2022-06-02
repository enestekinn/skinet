using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository,IMapper mapper){
            _mapper = mapper;

            _basketRepository = basketRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id){
            var basket = await _basketRepository.GetBasketAsync(id);

// if basket is null we return basket with id that client generated.
            return Ok(basket ?? new CustomerBasket(id));
        }
         [HttpPost]
         public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
         {

             var customerBasket = _mapper.Map<CustomerBasket>(basket);
             
             var  updatedBasket = await _basketRepository.UpdateBasketAsync(customerBasket);
             return Ok(updatedBasket);
         }
         [HttpDelete]
         public async Task DeleteBasketAsync(string id){
             await _basketRepository.DeleteBasketAsync(id);
         }
    }
}

/* 
we are not storing anything in our actual db  here is just a place where customers can leave their baskets 
behind in our memory so if  they come back , they can pick up where they let off.
we weill store basket's id in client side.
if they dont come back we will destroy their basket 
 */