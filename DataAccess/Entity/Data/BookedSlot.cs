using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity.Data
{
    [Table("BookedSlot")]
    public class BookedSlot : Entity
    {
        public bool CheckedIn { get; set; }

        [ForeignKey("Slot")]
        public string SlotId { get; set; }
        public Slot Slot { get; set; }
    }
}
