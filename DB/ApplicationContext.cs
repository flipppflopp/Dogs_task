using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace DB
{

    public class ApplicationContext : DbContext
    {
        public virtual DbSet<Dog> Dogs { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}