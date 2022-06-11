using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.OrderAggregate;
using Core.OrderAggregate;

namespace Core.Interfaces
{
    
    /*
     * genereate new order , get spesific order . the purpose of this is to keep the logic outside of our
     * controller. we  dont want our controller  to become too big. we work with diffrent repository  and make some calculation
     * we cant trust the basket that the client sends up to us 
     */
    public interface IOrderService
    {
     Task<Order> CreateOrderAsync(string buyerEmail,int deliveryMethod, string basketId,Address shippingAddress);
     Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
     Task<Order> GetOrderByIdAsync(int id, string buyerEmail);
     Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}