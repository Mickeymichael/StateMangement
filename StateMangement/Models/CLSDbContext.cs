using Microsoft.EntityFrameworkCore;

namespace StateMangement.Models
{
    public class CLSDbContext:DbContext
    {
        public CLSDbContext(DbContextOptions<CLSDbContext> options):base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }

    }
}
