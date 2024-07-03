using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity.Data
{
    [Table("MemberSubscription")]
    public class MemberSubscription : Entity
    {
        public Bill Bill { get; set; }
        [ForeignKey("Users")]
        public string MemberId { get; set; }
        public User Member { get; set; }

        [ForeignKey("SubscriptionOption")]
        public string SubscriptionOptionId { get; set; }
        public SubscriptionOption SubscriptionOption { get; set; }
    }
}
