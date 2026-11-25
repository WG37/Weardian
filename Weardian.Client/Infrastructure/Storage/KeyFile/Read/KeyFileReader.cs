using System.IO;

namespace Weardian.Client.Infrastructure.Storage.KeyFile.Read
{
    internal class KeyFileReader
    {
        private const int TagSize = 16;

        public static ParsedKeyFile ReadKeyFile(string path)
        {
            using var stream = File.OpenRead(path);
            using var reader = new BinaryReader(stream);

            byte[] headerBytes = ReadExactBytes(reader, KeyFileHeader.Size);
            KeyFileHeader header = ReadKeyFileHeader(headerBytes);

            uint expectedLength = KeyFileHeader.Size
                + header.CipherTextLength
                + TagSize;

            if (stream.Length != expectedLength)
                throw new InvalidDataException("File size does not match the expected header");

            byte[] cipherText = ReadExactBytes(reader, (int)header.CipherTextLength);
            byte[] tag = ReadExactBytes(reader, TagSize);

            return new ParsedKeyFile(header, headerBytes, cipherText, tag);
        }

        private static KeyFileHeader ReadKeyFileHeader(byte[] headerBytes)
        {
            if (headerBytes.Length != KeyFileHeader.Size)
                throw new InvalidDataException("Invalid key filer header size");

            using var stream = new MemoryStream(headerBytes, writable: false);
            using var reader = new BinaryReader(stream);

            // read magic bytes: offsets 0–3 (4 bytes)
            byte[] magic = ReadExactBytes(reader, KeyFileHeader.ExpectedMagic.Length);
            // checks if magic bytes are the same as ExpectedMagic
            if (!magic.AsSpan().SequenceEqual(KeyFileHeader.ExpectedMagic))
                throw new InvalidDataException("Invalid key file signature");

            // read version bytes: offset 4 (1 byte)
            byte version = reader.ReadByte();
            if (version != KeyFileHeader.CurrentVersion)
                throw new InvalidDataException("Unsupported key file version");

            // read salt bytes: offsets 5–20 (16 bytes)
            byte[] salt = ReadExactBytes(reader, KeyFileHeader.SaltSize);

            // read nonce bytes: offsets 21–32 (12 bytes)
            byte[] nonce = ReadExactBytes(reader, KeyFileHeader.NonceSize);

            // read ciphertext bytes: offsets 33–36 (4 bytes)
            uint cipherTextLength = reader.ReadUInt32();

            if (cipherTextLength < sizeof(uint) ||
                cipherTextLength > KeyFileHeader.MaxCipherTextLength)
            {
                throw new InvalidDataException("Invalid ciphertext length");
            }

            return new KeyFileHeader(version, salt, nonce, cipherTextLength);
        }

        private static byte[] ReadExactBytes(BinaryReader reader, int count)
        {
            byte[] bytesToRead = reader.ReadBytes(count);

            if (bytesToRead.Length != count)
                throw new EndOfStreamException("The byte length does not match the intended count");

            return bytesToRead;
        }
    }
}
