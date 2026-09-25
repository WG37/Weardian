namespace Weardian.Client.Core.DTOs.MessageHandler.HandleDelete
{
    public sealed record DeleteKeysRequestDto(
        List<Guid> KeyIds
        );
}
