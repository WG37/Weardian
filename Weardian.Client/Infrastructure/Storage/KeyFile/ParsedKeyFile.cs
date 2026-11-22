using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weardian.Client.Infrastructure.Storage.KeyFile
{
    internal sealed record ParsedKeyFile(
        KeyFileHeader Header,
        byte[] HeaderBytes,
        byte[] CipherText,
        byte[] Tag);
}
