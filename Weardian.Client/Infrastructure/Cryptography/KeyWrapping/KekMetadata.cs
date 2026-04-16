namespace Weardian.Client.Infrastructure.Cryptography.KeyWrapping
{
    internal sealed record KekMetadata(
        Guid KekId,
        int Version,
        DateTime CreatedOn);
}
