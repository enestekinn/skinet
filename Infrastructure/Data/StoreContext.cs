using System;
using System.Linq;
using System.Reflection;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext( DbContextOptions<StoreContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes {get; set;}

        
        // 
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

// OnModelCreating reponsible for creating migration
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            //base DbContext'den tureyen bir class
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly());

/*
HATASI ICIN KULLANIYORUZ
  SQLite cannot order by expressions of type 'decimal'. Convert the values to a supported type or use LINQ to Objects to order the results."
 decimal degeri  dobule a ceviriyoruz.

 SORU => 
 why are we using decimal over double in this case if decimal is not supported.
 CEVAP =>
 Decimal is only not supported in Sqlite which we only use for development.
  I didn’t want to build the app based on SQLite limitations as this is not the DB we would use in production.
   Double would work fine but you then need to convert it to currency backwards and forwards, it just seemed easier to work with decimals here.
*/
                if(Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
                {
                    foreach(var entityType in modelBuilder.Model.GetEntityTypes()){
                        var properties = entityType.ClrType
                        .GetProperties().Where(p => p.PropertyType == typeof(decimal));
                        
                        //Conversion DateTimeOffSet 
                        var dateTimeProperties = entityType.ClrType.GetProperties()
                            .Where(p => p.PropertyType == typeof(DateTimeOffset));
                        foreach(var property in properties){
                            modelBuilder.Entity(entityType.Name).Property(property.Name)
                            .HasConversion<double>();
                         
                        }

                        foreach (var property in dateTimeProperties)
                        {
                            modelBuilder.Entity(entityType.Name).Property(property.Name)
                                .HasConversion(new DateTimeOffsetToBinaryConverter());
                        }
                    }

                }
        }
    }
}
// Migration yapildiginda Product nesnesi ProductBrands,ProductTypes 
// icin  Foreign Key olusturacak.

/*
All this code is doing is telling Entity Framework where we have located our Entity configurations and to apply them from there.   I chose to separate the configurations from the DbContext class as this class can become quite large.    Our configurations make use of the IEntityTypeConfiguration<T>  interface and the combination of this interface, as well as telling where to apply the configurations from makes this code work. 

The GetExecutingAssembly part is just a way of saying that the configurations are in the same assembly that is executing the DbContext class (the Infrastructure.dll assembly).

Alternatively the configurations can be added directly to the OnModelCreating method by doing something like:

modelBuilder.Entity<Product>().Property(x => x.Name).IsRequired().

 */