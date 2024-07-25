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
    public class SubscriptionOptionService : ISubscriptionOptionService
    {
        ISubscriptionOptionRepository iSubscriptionOptionRepository;

        public SubscriptionOptionService(ISubscriptionOptionRepository iSubscriptionOptionRepository)
        {
            this.iSubscriptionOptionRepository = iSubscriptionOptionRepository;
        }

        public async Task<SubscriptionOption?> Create(SubscriptionOption entity)
        {
            try
            {
                return await iSubscriptionOptionRepository.Create(entity);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SubscriptionOption?> Delete(string id)
        {
            return await iSubscriptionOptionRepository.Delete(id);
        }

        public async Task<SubscriptionOption?> Get(string id)
        {
            return await iSubscriptionOptionRepository.Get(id);
        }

        public async Task<List<SubscriptionOption>?> Get()
        {
            return await iSubscriptionOptionRepository.Get();
        }

        public async Task<SubscriptionOption?> Update(SubscriptionOption entity)
        {
            return await iSubscriptionOptionRepository.Update(entity);
        }

        public async Task<List<SubscriptionOption>> GetByClubId(string clubId)
        {
            return await iSubscriptionOptionRepository.GetByClubId(clubId);
        }
    }
}
