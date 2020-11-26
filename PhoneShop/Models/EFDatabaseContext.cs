using Microsoft.EntityFrameworkCore;

namespace PhoneShop.Models.DataModel
{
    public class EFDatabaseContext:DbContext
    {
        public EFDatabaseContext(DbContextOptions<EFDatabaseContext> opts) : base(opts) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<ContactDetail> ContactDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasOne(s => s.Supplier).WithMany(p => p.Products).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
