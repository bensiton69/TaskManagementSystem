using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Core.Models;

namespace TaskManagementSystem.Persistence
{
    public class DataContext : DbContext
    {
        public DbSet<AppUser> Users { get; set; }
        public DbSet<SystemTask> SystemTasks { get; set; }
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }
    }
}
