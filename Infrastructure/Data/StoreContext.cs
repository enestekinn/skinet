using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

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

// OnModelCreating reponsible for creating migration
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            //base DbContext'den tureyen bir class
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly());
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

Thanks, 
 */