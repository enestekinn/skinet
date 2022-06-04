using System;
using System.Collections.Generic;
using Core.OrderAggregate;

namespace API.Dtos
{
    
    // json verimizi daha duzenli hale getirecegiz
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public Address ShipToAddress { get; set; }
        // string e cevirdik
        public string DeliveryMethod { get; set; }
        public  decimal ShippingPrice { get; set; }
        public IReadOnlyList<OrderItemDto> OrderItems { get; set; }
        public decimal Subtotal { get; set; }

        public decimal Total { get; set; }
        // string e cevirdik
        public string Status { get; set; }
        
    }
}