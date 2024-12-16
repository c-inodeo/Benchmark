using Microsoft.EntityFrameworkCore;

namespace BenchMarkSix
{ 
    public class ApplicationDbContext : DbContext
    { 
        public DbSet<UsersModel> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options) 
        {
                
        }
    }
}