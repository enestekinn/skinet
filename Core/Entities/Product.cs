namespace Core.Entities
{
    public class Product : BaseEntity
    {
/*
        BaseEntity class i olusturduk bi sayede  Id ye gerek kalmadi. 
        Fakat sirf bu yuzden onu eklemedik.Generic icin ekledik
 */
       // public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        //Related Entity
        public ProductType ProductType { get; set; }
        public int ProductTypeId { get; set; }
                //Related Entity
        public ProductBrand ProductBrand { get; set; }
        public int ProductBrandId { get; set; }
    }
}
// Migration yapildiginda Product in ProductType ile ProductBrand ile 
// iliskisi gozukecektir.