using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Weardian.Server.Domain.Keys.SymmetricKeys;

namespace Weardian.Server.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<SymmetricKey> SymmetricKeys { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ModelConfig.ConfigureSymmetricKeys(modelBuilder);
        }
    }
}
