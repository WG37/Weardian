namespace Weardian.Client.Core.Interfaces.Cryptography
{
    public interface IKeyGeneration
    {
        public byte[] GenerateSymmetricKey(int length = 32);
    }
}
