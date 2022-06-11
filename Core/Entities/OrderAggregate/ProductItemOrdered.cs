namespace Core.OrderAggregate
{
    
    /*
     * this is just going to act as a snapshot of our order at the time or  our product item at the time
     * Snapshot: In computer systems, a snapshot is the state of a system at a particular point in time.
     * Mesela bir urunu siparis verdik daha sonra o urunun ismini db den degistirdik burada biz siparis verilen
     * urunun resmini ismini degistirilmesini istemiyoruz.
     */
    public class ProductItemOrdered
    {
        public ProductItemOrdered(int productItemId, string productName, string pictureUrl)
        {
            ProductItemId = productItemId;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }

        public int ProductItemId  { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
    }
}