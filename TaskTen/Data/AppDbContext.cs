using FullStackSession6.Model;
using Microsoft.EntityFrameworkCore;
using TaskTen.Data.Entities;
using TaskTen.Model;

namespace TaskTen.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersConfigurations());
            modelBuilder.ApplyConfiguration(new TasksConfigurations());
            base.OnModelCreating(modelBuilder);
        }
    }
}
