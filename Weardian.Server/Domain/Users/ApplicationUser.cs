using Microsoft.AspNetCore.Identity;
using Weardian.Server.Domain.Authentication;
using Weardian.Server.Domain.EncryptedEnvelopes.Symmetric;
using Weardian.Server.Domain.KeyRecords.Symmetric;

namespace Weardian.Server.Domain.Users
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
        public ICollection<SymmetricEncryptedEnvelope> EncryptedEnvelopes { get; set; } = new List<SymmetricEncryptedEnvelope>();
    }
}
