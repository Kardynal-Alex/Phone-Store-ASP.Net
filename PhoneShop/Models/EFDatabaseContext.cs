using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace PhoneShop.Models
{
    public class EFDatabaseContext:DbContext
    {
        public EFDatabaseContext(DbContextOptions<EFDatabaseContext> opts) : base(opts) { }

        public DbSet<Product> Productss { get; set; }
    }
}
