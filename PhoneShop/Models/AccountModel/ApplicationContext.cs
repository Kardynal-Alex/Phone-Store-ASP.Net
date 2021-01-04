using Microsoft.EntityFrameworkCore;
namespace PhoneShop.Models.AccountModel
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> opt) : base(opt) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";
            string adminEmail = "admin";
            string adminPassword = "admin";

            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };
            User adminUser = new User { Id = 1, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });
            base.OnModelCreating(modelBuilder);
        }
    }
}
