using Microsoft.EntityFrameworkCore;
using SchoolERP.Domain.Entities.Utilities;

namespace SchoolERP.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets here
        public DbSet<User> Users { get; set; }
    }
}
