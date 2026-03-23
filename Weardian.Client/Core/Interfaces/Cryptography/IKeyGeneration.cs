namespace Weardian.Client.Core.Interfaces.Cryptography
{
    internal interface IKeyGeneration
    {
        public byte[] GenerateSymmetricKey(int length = 32);
    }
}
