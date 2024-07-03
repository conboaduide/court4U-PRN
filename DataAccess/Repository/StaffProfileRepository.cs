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
    public class StaffProfileRepository : IStaffProfileRepository
    {
        public async Task<StaffProfile> Create(StaffProfile StaffProfile)
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    _context.Add(StaffProfile);
                    _context.SaveChanges();
                    return StaffProfile;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<List<StaffProfile>> Get()
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    return await _context.StaffProfiles.ToListAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<StaffProfile?> Get(string id)
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    return await _context.StaffProfiles.Where(x => x.Id == id).FirstOrDefaultAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<StaffProfile> Update(StaffProfile StaffProfile)
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    _context.Update(StaffProfile);
                    _context.SaveChanges();
                    return StaffProfile;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<StaffProfile> Delete(string id)
        {
            using (var db = new Court4UDbContext())
            {
                var found = await db.StaffProfiles.Where(c => c.Id == id).SingleOrDefaultAsync();

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
