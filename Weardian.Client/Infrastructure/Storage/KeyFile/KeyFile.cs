using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weardian.Client.Infrastructure.Storage.KeyFile
{
    internal class KeyFile
    {
        private const int TagSize = 16;

        public static ParsedKeyFile ReadKeyFile(string path)
        {
            using var stream = File.OpenRead(path);
            using var reader = new BinaryReader(stream);

            
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
            byte[] bytes = reader.ReadBytes(count);

            if (bytes.Length != count)
                throw new EndOfStreamException("The byte length does not match the intended count");

            return bytes;
        }
    }
}
