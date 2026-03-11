using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Weardian.Server.Domain.Keys.Symmetric;
using Weardian.Server.Domain.Users;

namespace Weardian.Server.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
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
