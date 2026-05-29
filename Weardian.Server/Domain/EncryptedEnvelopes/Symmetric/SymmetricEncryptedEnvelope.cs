using Weardian.Server.Domain.KeyRecords.Symmetric;
using Weardian.Server.Domain.PayloadRecords.Symmetric;
using Weardian.Server.Domain.Users;
using Weardian.Server.Migrations;

namespace Weardian.Server.Domain.EncryptedEnvelopes.Symmetric
{
    public class SymmetricEncryptedEnvelope
    {
        public Guid EnvelopeId { get; set; }
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public required SymmetricKeyRecord KeyRecord { get; set; }
        public required SymmetricPayloadRecord PayloadRecord { get; set; }
    }
}
