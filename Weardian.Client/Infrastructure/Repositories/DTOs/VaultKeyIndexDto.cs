using Weardian.Client.Domain.KeyRecords;

namespace Weardian.Client.Infrastructure.Repositories.DTOs
{
    internal sealed record VaultKeyIndexDto(
        Guid KeyId,
        string Name,
        string Algorithm,
        KeyType KeyType,
        int KeySize,
        DateTime CreatedOn
        );
}
