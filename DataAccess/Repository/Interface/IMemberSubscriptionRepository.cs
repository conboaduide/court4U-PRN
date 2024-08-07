﻿using DataAccess.Entity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interface
{
    public interface IMemberSubscriptionRepository : IBaseRepository<MemberSubscription>
    {
        Task<MemberSubscription> GetByUserId(string userId);
        Task<List<MemberSubscription>> GetByClubId(string clubId);
    }
}
