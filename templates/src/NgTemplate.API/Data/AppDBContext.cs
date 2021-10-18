using Microsoft.EntityFrameworkCore;
using NgTemplate.API.Entities;

namespace NgTemplate.API.Data
{
    public class AppDBContext : DbContext
    {
        public DbSet<Demo> Demos { get; set; }
        public AppDBContext(DbContextOptions<AppDBContext> options)
                : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet((string)null, DelegationModes.ApplyToDatabases);
            base.OnModelCreating(modelBuilder);
        }
    }
}