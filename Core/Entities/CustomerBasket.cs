using System.Collections.Generic;

namespace Core.Entities
{

public class CustomerBasket 
{

    // we may need instance without needing id 
        public CustomerBasket()
        {
        }

        public CustomerBasket(string id)
        {
            Id = id;
        }

        // basket is a customer thing we let customer  generate the ID of the basket 
        public string Id { get; set; }
    public List<BasketItem> Items { get; set; } = new List<BasketItem>();
    public int? DeliveryMethodId { get; set; }
    public string ClientSecret { get; set; }
    public string PaymentIntentId { get; set; }
    public decimal ShippingPrice { get; set; }
}



}