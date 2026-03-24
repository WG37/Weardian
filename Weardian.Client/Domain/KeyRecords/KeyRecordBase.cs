namespace Weardian.Client.Domain.KeyRecords
{
    internal abstract class KeyRecordBase
    {
        public Guid LocalId { get; }
        public Guid PublicId { get; protected set; }
        public required string Name { get; set; }
        public required KeyType KeyType { get; init; }
        public KeyStatus KeyStatus { get; protected set; }


        public DateTime CreatedOn { get; protected set; }

        protected KeyRecordBase()
        {
            LocalId = Guid.NewGuid();
            KeyStatus = KeyStatus.Active;
            CreatedOn = DateTime.UtcNow;
        }
    }
}
