namespace Weardian.Client.Infrastructure.Storage.KeyFile
{
    internal sealed class KeyFileHeader
    {
        public const int Size = 37; 
        public const int SaltSize = 16;
        public const int NonceSize = 12;
        public const byte CurrentVersion = 1;
        public const uint MaxCipherTextLength = 16 * 1024 * 1024;

        public static ReadOnlySpan<byte> ExpectedMagic => "KVLT"u8;        // offsets 0–3 (4 bytes)
        public byte Version { get; }                               // offset 4 (1 byte)
        public byte[] Salt { get; }                                 // offsets 5–20 (16 bytes)
        public byte[] Nonce { get; }                                // offsets 21–32 (12 bytes)
        public uint CipherTextLength { get; }                       // offsets 33–36 (4 bytes)

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
