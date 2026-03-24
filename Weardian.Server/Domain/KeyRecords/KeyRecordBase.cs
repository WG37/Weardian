using Weardian.Server.Domain.Users;

namespace Weardian.Server.Domain.KeyRecords
{
    public abstract class KeyRecordBase
    {
        public Guid Id { get; private set; } 
        public Guid PublicId { get; protected set; }
        public required string Name { get; set; }
        public required KeyType KeyType { get; init; }
        public KeyStatus KeyStatus { get; protected set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime CreatedOn { get; protected set; }

        protected KeyRecordBase()
        {
            Id = Guid.NewGuid();
            PublicId = Guid.NewGuid();
            KeyStatus = KeyStatus.Active;
            CreatedOn = DateTime.UtcNow;
        }
    }
}
