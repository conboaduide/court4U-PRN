using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity.Data
{
    [Table("BookedSlot")]
    public class BookedSlot : Entity
    {
        public bool CheckedIn { get; set; }
        public float Price { get; set; }

        [ForeignKey("Slot")]
        public string SlotId { get; set; }
        public Slot Slot { get; set; }

        [ForeignKey("Booking")]
        public string BookingId { get; set; }
        public Booking Booking { get; set; }
    }
}
