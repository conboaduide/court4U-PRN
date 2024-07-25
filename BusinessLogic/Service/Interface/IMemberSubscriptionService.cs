﻿using DataAccess.Entity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service.Interface
{
    public interface IMemberSubscriptionService : IBaseService<MemberSubscription>
    {
        Task<MemberSubscription> GetByUserId(string userId);    
    }
}
