using DataAccess.Entity;
using DataAccess.Entity.Data;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemberSubscriptionRepository : IMemberSubscriptionRepository
    {
        public async Task<MemberSubscription> Create(MemberSubscription memberSubscription)
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    _context.Add(memberSubscription);
                    _context.SaveChanges();
                    return memberSubscription;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<List<MemberSubscription>> Get()
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    return await _context.MemberSubscriptions.ToListAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<MemberSubscription?> Get(string id)
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    return await _context.MemberSubscriptions.Where(x => x.Id == id).FirstOrDefaultAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        public async Task<MemberSubscription> Update(MemberSubscription memberSubscription)
        {
            using(var _context = new Court4UDbContext())
            {
                try
                {
                    _context.Update(memberSubscription);
                    _context.SaveChanges();
                    return memberSubscription;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<MemberSubscription> Delete(string id)
        {
            using(var _context = new Court4UDbContext())
            {
                try
                {
                    var found = await _context.MemberSubscriptions.Where(x => x.Id == id).FirstOrDefaultAsync();
                    if (found == null)
                    {
                        return null;
                    }
                    _context.MemberSubscriptions.Remove(found);
                    await _context.SaveChangesAsync();
                    return found;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
