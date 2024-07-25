using BusinessLogic.Service.Interface;
using DataAccess.Entity.Data;
using DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service
{
    public class MemberSubscriptionService : IMemberSubscriptionService
    {
        IMemberSubscriptionRepository iMemberSubscriptionRepository;

        public MemberSubscriptionService(IMemberSubscriptionRepository iMemberSubscriptionRepository)
        {
            this.iMemberSubscriptionRepository = iMemberSubscriptionRepository;
        }

        public async Task<MemberSubscription?> Create(MemberSubscription entity)
        {
            return await iMemberSubscriptionRepository.Create(entity);
        }

        public async Task<MemberSubscription?> Delete(string id)
        {
            return await iMemberSubscriptionRepository.Delete(id);
        }

        public async Task<MemberSubscription?> Get(string id)
        {
            return await iMemberSubscriptionRepository.Get(id);
        }

        public async Task<List<MemberSubscription>?> Get()
        {
            return await iMemberSubscriptionRepository.Get();
        }

        public async Task<MemberSubscription?> Update(MemberSubscription entity)
        {
            return await iMemberSubscriptionRepository.Update(entity);
        }
        public async Task<MemberSubscription> GetByUserId(string userId)
        {
            return await iMemberSubscriptionRepository.GetByUserId(userId);
        }

        public async Task<List<MemberSubscription>> GetByClubId(string clubId)
        {
            return await iMemberSubscriptionRepository.GetByClubId(clubId);
        }
    }
}
