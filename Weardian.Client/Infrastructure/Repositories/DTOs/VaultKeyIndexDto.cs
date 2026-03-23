using Weardian.Client.Domain.Keys;

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
