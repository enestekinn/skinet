using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    /* 
    Bu class in olayi migration yaptigimizda  nullable true geliyo 
    biz bunu istemiyorsak burada migrationi configure yapacagiz. 
     */
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

// her degeri tek tek ele aliyoruz id yi yapmamiza gerek yoktu.
         builder.Property(p => p.Id).IsRequired();
         builder.Property(p => p.Name).IsRequired().HasMaxLength(100);   
         builder.Property(p => p.Description).IsRequired();
         builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
         builder.Property(p => p.PictureUrl).IsRequired();
         builder.HasOne(b => b.ProductBrand)
         .WithMany().HasForeignKey(p => p.ProductBrandId);
         builder.HasOne(t => t.ProductType).WithMany()
         .HasForeignKey(p => p.ProductTypeId);
        }
    }
}
/* Decimal(x,y): Ondalık sayıları tutmaya yarar.
X: sayının kaç karakter olduğunu gösterir.
Y: kaç karakteri ondalıklı kısımda olduğunu gösterir.

 Product in birden fazla ProductBrandi olabilir.
 Ve ProductBrandId var mi 
 */