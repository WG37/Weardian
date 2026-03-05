namespace Weardian.Server.Domain.Keys
{
    public abstract class KeyBase
    {
        public Guid Id { get; protected set; } 
        public Guid PublicId { get; protected set; }
        public required string Name { get; set; }
        public required KeyType KeyType { get; init; }
        public KeyStatus KeyStatus { get; protected set; }
        public DateTime CreatedOn { get; protected set; }

        protected KeyBase()
        {
            Id = Guid.NewGuid();
            PublicId = Guid.NewGuid();
            KeyStatus = KeyStatus.Active;
            CreatedOn = DateTime.UtcNow;
        }
    }
}
