using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity.Data
{
    [Table("Bill")]
    public class Bill : Entity
    {
        public string Method { get; set; }
        public float Price { get; set; }
        public string Type { get; set; }
        public virtual Booking Booking { get; set; }
        public MemberSubscription MemberSubscription { get; set; }
    }
}
