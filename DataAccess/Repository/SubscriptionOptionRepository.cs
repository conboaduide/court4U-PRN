using DataAccess.Entity.Data;
using DataAccess.Entity;
using DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class SubscriptionOptionRepository : ISubscriptionOptionRepository
    {
        public async Task<SubscriptionOption> Create(SubscriptionOption subscriptionOption)
        {
            using(var _context = new Court4UDbContext())
            {
                try
                {
                    _context.Add(subscriptionOption);
                    _context.SaveChanges();
                    return subscriptionOption;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<List<SubscriptionOption>> Get()
        {
           using(var _context = new Court4UDbContext())
            {
                try
                {
                    return await _context.SubscriptionOptions.ToListAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<SubscriptionOption?> Get(string id)
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    return await _context.SubscriptionOptions.Where(x => x.Id == id).FirstOrDefaultAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<SubscriptionOption> Update(SubscriptionOption subscriptionOption)
        {
           using(var _context = new Court4UDbContext())
            {
                try
                {
                    _context.Update(subscriptionOption);
                    _context.SaveChanges();
                    return subscriptionOption;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<SubscriptionOption> Delete(string id)
        {
            using (var db = new Court4UDbContext())
            {
                var found = await db.SubscriptionOptions.Where(c => c.Id == id).SingleOrDefaultAsync();

                if (found != null)
                {
                    db.Remove(found);
                    await db.SaveChangesAsync();
                    return found;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
