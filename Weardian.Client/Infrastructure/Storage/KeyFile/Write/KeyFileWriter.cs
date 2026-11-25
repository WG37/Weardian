using System.IO;

namespace Weardian.Client.Infrastructure.Storage.KeyFile.Write
{
    internal class KeyFileWriter
    {
        public static byte[] WriteKeyFileHeader(KeyFileHeader header)
        {
            if (header.Version != KeyFileHeader.CurrentVersion)
                throw new InvalidDataException("Unsupported key file version");

            if (header.Salt.Length != KeyFileHeader.SaltSize)
                throw new InvalidDataException("Invalid salt length");

            if (header.Nonce.Length != KeyFileHeader.NonceSize)
                throw new InvalidDataException("Invalid nonce length");

            if (header.CipherTextLength < sizeof(uint) ||
                header.CipherTextLength > KeyFileHeader.MaxCipherTextLength)
            {
                throw new InvalidDataException("Invalid ciphertext length");
            }

            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);

            writer.Write(KeyFileHeader.ExpectedMagic);
            writer.Write(header.Version);
            writer.Write(header.Salt);
            writer.Write(header.Nonce);
            writer.Write(header.CipherTextLength);

            return stream.ToArray();
        }

        public static void WriteKeyFile(string path, byte[] headerBytes, byte[] cipherText, byte[] tag)
        {
            using var filestream = File.Create(path);
            using var writer = new BinaryWriter(filestream);

            writer.Write(headerBytes);
            writer.Write(cipherText);
            writer.Write(tag);
        }
    }
}
