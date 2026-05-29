using Microsoft.EntityFrameworkCore;
using Weardian.Server.Domain.EncryptedEnvelopes.Symmetric;
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
            modelBuilder.Entity<SymmetricEncryptedEnvelope>(e =>
            {
                e.ToTable("SymmetricEncryptedEnvelopes");

                e.HasKey(envelope => envelope.EnvelopeId);

                e.HasOne(envelope => envelope.User)
                 .WithMany(user => user.EncryptedEnvelopes)
                 .HasForeignKey(envelope => envelope.UserId)
                 .OnDelete(DeleteBehavior.Cascade);

                e.OwnsOne(envelope => envelope.KeyRecord, key =>
                {
                    key.ToTable("SymmetricKeyRecords");

                    key.WithOwner().HasForeignKey(k => k.EnvelopeId);
                    key.HasKey(k => k.EnvelopeId);

                    key.Property(k => k.EnvelopeId).IsRequired();

                    key.Property(k => k.Name).IsRequired().HasMaxLength(32);
                    key.Property(k => k.KeyType).IsRequired();
                    key.Property(k => k.KeyStatus).IsRequired();

                    key.Property(k => k.CreatedOn).IsRequired();

                    key.Property(k => k.WrappedKeyCiphertext).IsRequired()
                        .HasConversion(
                        b => b.ToArray(),
                        b => new ReadOnlyMemory<byte>(b));

                    key.Property(k => k.EnvelopeVersion).IsRequired();
                    key.Property(k => k.WrapAlgorithm).IsRequired();

                    key.Property(k => k.WrappingKeyId).IsRequired().ValueGeneratedNever();
                    key.HasIndex(k => k.WrappingKeyId);

                    key.Property(k => k.WrappedKeyTag).IsRequired();
                    key.Property(k => k.WrappedKeyNonce).IsRequired();
                });

                e.OwnsOne(envelope => envelope.PayloadRecord, payload =>
                {
                    payload.ToTable("SymmetricPayloadRecords");

                    payload.WithOwner().HasForeignKey(p => p.EnvelopeId);
                    payload.HasKey(p => p.EnvelopeId);

                    payload.Property(p => p.EnvelopeId).IsRequired();

                    payload.Property(p => p.Name).IsRequired().HasMaxLength(32);
                    payload.Property(p => p.KeyType).IsRequired();
                    payload.Property(p => p.KeyStatus).IsRequired();

                    payload.Property(p => p.CreatedOn).IsRequired();

                    payload.Property(p => p.Ciphertext).IsRequired()
                            .HasConversion(
                            b => b.ToArray(),
                            b => new ReadOnlyMemory<byte>(b));

                    payload.Property(p => p.Version).IsRequired();
                    payload.Property(p => p.Algorithm).IsRequired();
                    payload.Property(p => p.Nonce).IsRequired();
                    payload.Property(p => p.Tag).IsRequired();
                });

            });
        }
    }
}
