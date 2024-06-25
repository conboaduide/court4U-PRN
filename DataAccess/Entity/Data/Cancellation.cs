using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity.Data
{
    [Table("Cancellation")]
    public class Cancellation
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public BookedSlot BookedSlot { get; set; }

        [ForeignKey("Users")]
        public string CancellerId { get; set; }
        public User Canceller { get; set; }
    }
}
