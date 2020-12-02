using Microsoft.EntityFrameworkCore;

namespace PhoneShop.Models.DataModel
{
    public class EFDatabaseContext:DbContext
    {
        public EFDatabaseContext(DbContextOptions<EFDatabaseContext> opts) : base(opts) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<ContactDetail> ContactDetails { get; set; }
        public DbSet<ProductInfo> ProductInfos { get; set; }
        public DbSet<PromoCodeSystem> PromoCodeSystems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasOne(p => p.ProductInfo).WithOne(p => p.Product).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Product>().HasOne(s => s.Supplier).WithMany(p => p.Products).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<PromoCodeSystem>().HasKey(u => u.Id);
            modelBuilder.Entity<PromoCodeSystem>().Property(b => b.PromoCode).IsRequired();
            modelBuilder.Entity<PromoCodeSystem>().Property(b => b.Date1).IsRequired();
            modelBuilder.Entity<PromoCodeSystem>().Property(b => b.Date2).IsRequired();
            modelBuilder.Entity<PromoCodeSystem>().Property(b => b.DiscountPercentage).IsRequired();
        }
    }
}
