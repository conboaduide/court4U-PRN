using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity.Data
{
    [Table("Booking")]
    public class Booking : Entity
    {
        public bool Status { get; set; }
        public float Price { get; set; }
        public string BillId { get; set; }
        public DateTime Date { get; set; }
        public bool CheckIn {  get; set; } = false;
        public string SlotId { get; set; }
        public Slot Slot { get; set; }

        public Bill Bill { get; set; }

        [ForeignKey("Users")]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
