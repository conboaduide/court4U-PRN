using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity.Data
{
    [Table("SubscriptionOption")]
    public class SubscriptionOption : Entity
    {
        public string Name { get; set; }
        public float price { get; set; }
        public int TotalDate { get; set; }
        public Enums.SubscriptionOptionStatus Status { get; set; }
        public ICollection<MemberSubscription> MemberSubscriptions { get; set; }
        public ICollection<SubOptionSlot> SubOptionSlots { get; set; }

        [ForeignKey("Clubs")]
        public string ClubId { get; set; }
        public Club Club { get; set; }
    }
}
