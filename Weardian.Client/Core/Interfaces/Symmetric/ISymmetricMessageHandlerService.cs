namespace Weardian.Client.Core.Interfaces.Symmetric
{
    public interface ISymmetricMessageHandlerService
    {
        public Task<string> HandleAsync(string request);
        public Task<string> HandleEncryptionRequestAsync(string request);
        public Task<string> HandleDecryptionRequestAsync(string request);
        public Task<string> HandleRetrieveAllKeysRequestAsync();
        public string HandleDeleteKeyRequest(string request);
    }
}
