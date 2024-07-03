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
    public class SlotRepository : ISlotRepository
    {
        public async Task<Slot> Create(Slot Slot)
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    _context.Add(Slot);
                    _context.SaveChanges();
                    return Slot;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<List<Slot>> Get()
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    return await _context.Slots.ToListAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<Slot?> Get(string id)
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    return await _context.Slots.Where(x => x.Id == id).FirstOrDefaultAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<Slot> Update(Slot Slot)
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    _context.Update(Slot);
                    _context.SaveChanges();
                    return Slot;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<Slot> Delete(string id)
        {
            using (var db = new Court4UDbContext())
            {
                var found = await db.Slots.Where(c => c.Id == id).SingleOrDefaultAsync();

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
