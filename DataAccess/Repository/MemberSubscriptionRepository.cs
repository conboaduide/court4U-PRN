using DataAccess.Entity;
using DataAccess.Entity.Data;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemberSubscriptionRepository : IMemberSubscriptionRepository
    {
        private readonly Court4UDbContext _dbContext;

        public MemberSubscriptionRepository(Court4UDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<MemberSubscription?> Create(MemberSubscription entity)
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

        public async Task<MemberSubscription?> Delete(string id)
        {
            try
            {
                var memberSubscription = await _dbContext.MemberSubscriptions.FindAsync(id);
                if (memberSubscription != null)
                {
                    _dbContext.MemberSubscriptions.Remove(memberSubscription);
                    await _dbContext.SaveChangesAsync();
                }
                return memberSubscription;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<MemberSubscription?> Get(string id)
        {
            try
            {
                return await _dbContext.MemberSubscriptions.FirstOrDefaultAsync(ms => ms.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<MemberSubscription>> Get()
        {
            try
            {
                return await _dbContext.MemberSubscriptions.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<MemberSubscription?> Update(MemberSubscription entity)
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
        public async Task<MemberSubscription> GetByUserId(string userId)
        {
            try
            {
                var result = await _dbContext.MemberSubscriptions.Include(x => x.SubscriptionOption).FirstOrDefaultAsync(x => x.MemberId == userId);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
