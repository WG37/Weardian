using Weardian.Client.Domain.Enums;

namespace Weardian.Client.Domain.PayloadRecords
{
    public abstract class PayloadBase
    {
        public string Name { get; set; }
        public KeyType KeyType { get; init; }
        public KeyStatus KeyStatus { get; protected set; }
        public DateTime CreatedOn { get; protected set; }

        public PayloadBase()
        {
            KeyStatus = KeyStatus.Active;
            CreatedOn = DateTime.UtcNow;
        }
    }
}
