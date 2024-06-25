using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity.Data
{
    [Table("BookedSlot")]
    public class BookedSlot
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime Date { get; set; }
        public bool CheckedIn { get; set; }
        public Cancellation Cancellation { get; set; }

        [ForeignKey("Slot")]
        public string SlotId { get; set; }
        public Slot Slot { get; set; }
    }
}
