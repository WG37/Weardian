namespace Weardian.Client.Infrastructure.Repositories.DTOs
{
    internal sealed record VaultSettingsDto(
        Guid VaultId,
        int SchemaVersion,
        DateTime CreatedOn
        );
    
}
