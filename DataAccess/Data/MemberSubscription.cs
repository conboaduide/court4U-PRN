﻿using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Data
{
    [Table("MemberSubscription")]
    public class MemberSubscription
    {
        public Bill Bill { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        [ForeignKey("Users")]
        public string MemberId { get; set; }
        public User Member { get; set; }

        [ForeignKey("SubscriptionOption")]
        public string SubscriptionOptionId { get; set; }
        public SubscriptionOption SubscriptionOption { get; set; }
    }
}
