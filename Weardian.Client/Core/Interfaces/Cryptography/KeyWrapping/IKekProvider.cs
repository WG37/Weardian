namespace Weardian.Client.Core.Interfaces.Cryptography.KeyWrapping
{
    public interface IKekProvider
    {
        public byte[] GetKek();
        public Task<byte[]> CreateKekAsync();
        public Task<byte[]> GetOrCreateKekAsync();
        public Guid GetKekId();
    }
}
