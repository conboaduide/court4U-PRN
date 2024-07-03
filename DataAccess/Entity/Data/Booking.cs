using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity.Data
{
    [Table("Booking")]
    public class Booking : Entity
    {
        public bool Status { get; set; }
        public Bill Bill { get; set; }

        [ForeignKey("Users")]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
