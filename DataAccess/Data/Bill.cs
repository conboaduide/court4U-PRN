using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Data
{
    [Table("Bill")]
    public class Bill
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Method { get; set; }
        public float Price { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public Booking Booking { get; set; }
        public MemberSubscription MemberSubscription { get; set; }
    }
}
