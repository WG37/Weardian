using Weardian.Server.Domain.Users;

namespace Weardian.Server.Domain.Authentication
{
    public class RefreshToken
    {
        public Guid Id { get; init; }
        public required string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public required string HashedToken { get; set; }

        public DateTime? RevokedAt { get; set; }
        public DateTime Expiry { get; set; }
    }
}
