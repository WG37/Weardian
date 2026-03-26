using Microsoft.EntityFrameworkCore;
using Weardian.Server.Domain.KeyRecords.Symmetric;

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
            modelBuilder.Entity<SymmetricKeyRecord>(e =>
            {
                e.ToTable("SymmetricKeyRecords");
                e.HasKey(k => k.Id);
                e.Property(k => k.Id).ValueGeneratedNever();

                e.HasIndex(k => k.EnvelopeId).IsUnique();

                e.Property(k => k.Name).IsRequired().HasMaxLength(32);
                e.Property(k => k.KeyType).IsRequired();
                e.Property(k => k.KeyStatus).IsRequired();

                e.HasOne(k => k.User)
                 .WithMany(u => u.Keys)
                 .HasForeignKey(k => k.UserId)
                 .OnDelete(DeleteBehavior.Cascade);

                e.Property(k => k.CreatedOn).IsRequired();

                e.Property(k => k.WrappedKeyCiphertext).IsRequired()
                    .HasConversion(
                    b => b.ToArray(),
                    b => new ReadOnlyMemory<byte>(b));

                e.Ignore(k => k.KeyLength);

                e.Property(k => k.EnvelopeVersion).IsRequired();
                e.Property(k => k.WrapAlgorithm).IsRequired();

                e.Property(k => k.WrappingKeyId).IsRequired().ValueGeneratedNever();
                e.HasIndex(k => k.WrappingKeyId);

                e.Property(k => k.WrappedKeyTag).IsRequired();
                e.Property(k => k.WrappedKeyNonce).IsRequired();
            });
        }
    }
}
