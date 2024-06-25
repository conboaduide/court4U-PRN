using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Data
{
    [Table("Slot")]
    public class Slot
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime DateOfWeek { get; set; }
        public ICollection<SubOptionSlot> SubOptionSlots { get; set; }
        public ICollection<BookedSlot> BookedSlots { get; set; }

        [ForeignKey("Clubs")]
        public string ClubId { get; set; }
        public Club Club { get; set; }
    }
}
