using Microsoft.EntityFrameworkCore;

namespace PhoneShop.Models
{
    public class EFDatabaseContext:DbContext
    {
        public EFDatabaseContext(DbContextOptions<EFDatabaseContext> opts) : base(opts) { }

        public DbSet<Product> Productss { get; set; }
    }
}
