namespace Weardian.Client.Core.Interfaces.Handler
{
    public interface IMessageHandlerService
    {
        public Task<string> HandleAsync(string request);
        public Task<string> HandleRegistrationRequestAsync(string request);
        public Task<string> HandleLoginRequestAsync(string request);
        public string HandleLogoutRequest();
        public Task<string> HandleUpdateUserPasswordAsync(string request);
        public Task<string> HandleEncryptionRequestAsync(string request);
        public Task<string> HandleDecryptionRequestAsync(string request);
        public Task<string> HandleRetrieveAllKeysRequestAsync();
        public Task<string> HandleDeleteKeysRequest(string request);
    }
}
