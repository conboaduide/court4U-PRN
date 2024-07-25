using DataAccess.Entity;
using DataAccess.Entity.Data;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class SubscriptionOptionRepository : ISubscriptionOptionRepository
    {
        private readonly Court4UDbContext _dbContext;

        public SubscriptionOptionRepository(Court4UDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SubscriptionOption?> Create(SubscriptionOption entity)
        {
            try
            {
                _dbContext.Add(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SubscriptionOption?> Delete(string id)
        {
            try
            {
                var subscriptionOption = await _dbContext.SubscriptionOptions.FindAsync(id);
                if (subscriptionOption != null)
                {
                    _dbContext.SubscriptionOptions.Remove(subscriptionOption);
                    await _dbContext.SaveChangesAsync();
                }
                return subscriptionOption;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SubscriptionOption?> Get(string id)
        {
            try
            {
                return await _dbContext.SubscriptionOptions.FirstOrDefaultAsync(so => so.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<SubscriptionOption>> Get()
        {
            try
            {
                return await _dbContext.SubscriptionOptions.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SubscriptionOption?> Update(SubscriptionOption entity)
        {
            try
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<SubscriptionOption>> GetByClubId(string clubId)
        {
            try
            {
                
                var result = await _dbContext.SubscriptionOptions.Where(x => x.ClubId == clubId).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
