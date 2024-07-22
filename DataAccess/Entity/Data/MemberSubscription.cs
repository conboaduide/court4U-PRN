using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity.Data
{
    [Table("MemberSubscription")]
    public class MemberSubscription : Entity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float Price { get; set; }
        public string BillId { get; set; }
        public Bill Bill { get; set; }
        [ForeignKey("Users")]
        public string MemberId { get; set; }
        public User Member { get; set; }

        [ForeignKey("SubscriptionOption")]
        public string SubscriptionOptionId { get; set; }
        public SubscriptionOption SubscriptionOption { get; set; }
    }
}
