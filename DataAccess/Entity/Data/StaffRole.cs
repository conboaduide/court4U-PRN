using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity.Data
{
    [Table("StaffRole")]
    public class StaffRole
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey("Users")]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
