using Microsoft.AspNetCore.Identity;
using Weardian.Server.Domain.KeyRecords.Symmetric;

namespace Weardian.Server.Domain.Users
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }

        public ICollection<SymmetricKeyRecord> Keys { get; set; } = new List<SymmetricKeyRecord>();
    }
}
