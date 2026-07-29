namespace Weardian.Client.Core.DTOs.Cryptography
{
    public sealed record EncryptedPayloadRecordDto(
        Guid EnvelopeId,
        string Name,
        string Algorithm,
        byte[] Ciphertext,
        byte[] Nonce,
        byte[] Tag,
        DateTime CreatedOn);
}
