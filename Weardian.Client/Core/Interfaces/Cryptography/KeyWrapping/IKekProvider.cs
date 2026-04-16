namespace Weardian.Client.Core.Interfaces.Cryptography.KeyWrapping
{
    internal interface IKekProvider
    {
        public byte[] GetKek();
        public Task<byte[]> CreateKekAsync();
        public Task<byte[]> GetOrCreateKekAsync();
        public Guid GetKekId();
    }
}
