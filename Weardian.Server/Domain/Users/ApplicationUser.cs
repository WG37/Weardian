using Microsoft.AspNetCore.Identity;
using Weardian.Server.Domain.EncryptedEnvelopes.Symmetric;
using Weardian.Server.Domain.KeyRecords.Symmetric;

namespace Weardian.Server.Domain.Users
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<SymmetricEncryptedEnvelope> EncryptedEnvelopes = new List<SymmetricEncryptedEnvelope>();
    }
}
