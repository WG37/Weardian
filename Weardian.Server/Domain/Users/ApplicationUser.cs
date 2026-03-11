using Microsoft.AspNetCore.Identity;
using Weardian.Server.Domain.Keys.Symmetric;

namespace Weardian.Server.Domain.Users
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }

        public ICollection<SymmetricKey> Keys { get; set; } = new List<SymmetricKey>();
    }
}
