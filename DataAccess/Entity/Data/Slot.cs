using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity.Data
{
    [Table("Slot")]
    public class Slot : Entity
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Enums.DateOfWeek DateOfWeek { get; set; }
        public float Price { get; set; }
        public Booking Booking { get; set; }
        public ICollection<SubOptionSlot> SubOptionSlots { get; set; }

        [ForeignKey("Clubs")]
        public string ClubId { get; set; }
        public Club Club { get; set; }
    }
}
