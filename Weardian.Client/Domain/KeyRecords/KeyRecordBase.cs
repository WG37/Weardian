namespace Weardian.Client.Domain.KeyRecords
{
    internal abstract class KeyRecordBase
    {

        public string Name { get; set; }
        public KeyType KeyType { get; init; }
        public KeyStatus KeyStatus { get; protected set; }


        public DateTime CreatedOn { get; protected set; }

        protected KeyRecordBase()
        {
            KeyStatus = KeyStatus.Active;
            CreatedOn = DateTime.UtcNow;
        }
    }
}
