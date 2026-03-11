using Microsoft.EntityFrameworkCore;
using Weardian.Server.Domain.Keys.Symmetric;

namespace Weardian.Server.Infrastructure.Data
{
    public static class ModelConfig
    {
        public static void ConfigureModel(ModelBuilder modelBuilder)
        {
            ConfigureSymmetricKeys(modelBuilder);
        }

        public static void ConfigureSymmetricKeys(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SymmetricKey>(e =>
            {
                e.ToTable("SymmetricKeys");
                e.HasKey(k => k.Id);
                e.Property(k => k.Id).ValueGeneratedNever();

                e.HasIndex(k => k.PublicId).IsUnique();

                e.Property(k => k.Name).IsRequired().HasMaxLength(32);
                e.Property(k => k.KeyType).IsRequired();
                e.Property(k => k.KeyStatus).IsRequired();

                e.HasOne(k => k.User)
                 .WithMany(u => u.Keys)
                 .HasForeignKey(k => k.UserId)
                 .OnDelete(DeleteBehavior.Cascade);

                e.Property(k => k.CreatedOn).IsRequired();

                e.Property(k => k.Ciphertext).IsRequired()
                    .HasConversion(
                    b => b.ToArray(),
                    b => new ReadOnlyMemory<byte>(b));

                e.Ignore(k => k.KeyLength);

                e.Property(k => k.EnvelopeVersion).IsRequired();
                e.Property(k => k.WrapAlgorithm).IsRequired();

                e.Property(k => k.WrappingKeyId).IsRequired().ValueGeneratedNever();
                e.HasIndex(k => k.WrappingKeyId);

                e.Property(k => k.Tag).IsRequired();
                e.Property(k => k.Nonce).IsRequired();
            });
        }
    }
}
