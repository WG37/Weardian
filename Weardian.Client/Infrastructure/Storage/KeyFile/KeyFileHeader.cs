namespace Weardian.Client.Infrastructure.Storage.KeyFile
{
    internal sealed class KeyFileHeader
    {
        public const int Size = 37; 
        public const int SaltSize = 16;
        public const int NonceSize = 12;
        public const byte CurrentVersion = 1;
        public const uint MaxCipherTextLength = 16 * 1024 * 1024;

        public static ReadOnlySpan<byte> Magic => "KVLT"u8;        // offset 0 - 4 bytes
        public byte Version { get; }                               // offset 4 - 1 bytes
        public byte[] Salt { get; }                                 // offset 5 - 16 bytes
        public byte[] Nonce { get; }                                // offset 21 - 12 bytes
        public uint CipherTextLength { get; }                       // offset 33 - 4 bytes

        public KeyFileHeader(
            byte version, 
            byte[] salt, 
            byte[] nonce, 
            uint cipherTextLength)
        {
            Version = version;
            Salt = salt;
            Nonce = nonce;
            CipherTextLength = cipherTextLength;
        }
    }
}
