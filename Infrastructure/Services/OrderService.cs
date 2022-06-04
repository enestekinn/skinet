using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.OrderAggregate;
using Core.Specifications;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {

        
        // BEFORE UNIT OF WORK 
        
        // private readonly IGenericRepository<Order> _orderRepo;
        // private readonly IGenericRepository<Product> _productRepo;
        // private readonly IBasketRepository _basketRepo;
        // private readonly IGenericRepository<DeliveryMethod> _dmRepo;
        // public OrderService(
        //     IGenericRepository<Order> orderRepo,
        //         IGenericRepository<DeliveryMethod> dmRepo,
        //         IGenericRepository<Product> productRepo,
        //         IBasketRepository basketRepo
        //     )
        // {
        //     _orderRepo = orderRepo;
        //     _productRepo = productRepo;
        //     _basketRepo = basketRepo;
        //     _dmRepo = dmRepo;
        //
        // }
        
        // AFTER UNIT OF WORK 
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IBasketRepository basketRepo, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _basketRepo = basketRepo;

        }
        
        
        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            //get basket from the repo
            var basket = await _basketRepo.GetBasketAsync(basketId);
            // get items from the product repo
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                //var productItem = await _productRepo.GetByIdAsync(item.Id);
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }
            // get delivery method 
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
            //cal subtotal
            var subtotal = items.Sum(item => item.Price * item.Quantity);
            //create order 
            var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotal);
            _unitOfWork.Repository<Order>().Add(order);
            
            //  save to db
            var result = await _unitOfWork.Complete();

            if (result <= 0) // nothings been saved into db
            {
                return null;
            }
            // delete basket 
            await _basketRepo.DeleteBasketAsync(basketId);
            
            //return order 
            return order;







        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);
            return await _unitOfWork.Repository<Order>().ListAsync(spec);
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(id, buyerEmail);
                return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec) ;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
        }
    }
}   

/*
 *SORU => whats the difference between a Repository and a Service? 
 *
 *CEVAP => Typically a repository will contain code that accesses or updates the DB.
 * A service will typically be used for anything that may be used across multiple classes that does not need DB access and/or uses 3rd party services.  
 * 
             *  we do not trust what's in the basket .
             * Bir magazada fiyat degistirildigin satici fiyata guvenmez onu scannerdan gecirerek fiyatini tekrar kontrol eder
             * bizde burda boyle bir method uygulayacagiz
             */