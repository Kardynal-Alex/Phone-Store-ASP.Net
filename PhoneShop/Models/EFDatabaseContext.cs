using Microsoft.EntityFrameworkCore;

namespace PhoneShop.Models.DataModel
{
    public class EFDatabaseContext:DbContext
    {
        public EFDatabaseContext(DbContextOptions<EFDatabaseContext> opts) : base(opts) { }

        public DbSet<Product> Products { get; set; }
    }
}
