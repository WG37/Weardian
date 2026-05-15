using Weardian.Client.Core.DTOs.MessageHandlerDtos;

namespace Weardian.Client.Core.Interfaces.Symmetric
{
    public interface ISymmetricMessageHandlerService
    {
        public Task<string> HandleEncryptionRequestAsync(string request);
    }
}
