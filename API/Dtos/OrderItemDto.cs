using Core.OrderAggregate;

namespace API.Dtos
{
    // json verimizi daha duzenli hale getirecegiz
    public class OrderItemDto
    {
      
        public int ProductId  { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}