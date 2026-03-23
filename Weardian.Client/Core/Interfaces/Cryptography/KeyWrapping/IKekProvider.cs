namespace Weardian.Client.Core.Interfaces.Cryptography.KeyWrapping
{
    internal interface IKekProvider
    {
        public byte[] CreateKek();
        public byte[] GetKek();
        public byte[] GetOrCreateKek();
    }
}
