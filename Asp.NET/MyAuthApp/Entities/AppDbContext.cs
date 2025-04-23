using Microsoft.EntityFrameworkCore;

namespace MyAuthApp.Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Add any additional configuration here
        }
        public DbSet<UserAccount> UserAccounts { get; set; }
    }
   
}
