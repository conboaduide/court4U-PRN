using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity.Data
{
    [Table("SubOptionSlot")]
    public class SubOptionSlot
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey("Slot")]
        public string SlotId { get; set; }
        public Slot Slot { get; set; }

        [ForeignKey("SubscriptionOption")]
        public string SubscriptionOptionId { get; set; }
        public SubscriptionOption SubscriptionOption { get; set; }
    }
}
