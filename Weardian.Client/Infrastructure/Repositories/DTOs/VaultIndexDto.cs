namespace Weardian.Client.Infrastructure.Repositories.DTOs
{
    internal sealed record VaultIndexDto
    {
        public int SchemaVersion { get; set; }
        public DateTime CreatedOn { get; set; }
        public VaultKeyIndexDto[] Keys { get; set; } = Array.Empty<VaultKeyIndexDto>();
    }
}
