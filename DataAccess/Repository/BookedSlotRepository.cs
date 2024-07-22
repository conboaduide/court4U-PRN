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
    public class BookedSlotRepository : IBookedSlotRepository
    {
        public async Task<BookedSlot> Create(BookedSlot bill)
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    _context.Add(bill);
                    _context.SaveChanges();
                    return bill;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<List<BookedSlot>> Get()
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    return await _context.BookedSlots.ToListAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<BookedSlot?> Get(string id)
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    return await _context.BookedSlots.Where(x => x.Id == id).FirstOrDefaultAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<BookedSlot> Update(BookedSlot bookedSlot)
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    _context.Update(bookedSlot);
                    _context.SaveChanges();
                    return bookedSlot;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<BookedSlot> Delete(string id)
        {
            using (var db = new Court4UDbContext())
            {
                var found = await db.BookedSlots.Where(c => c.Id == id).SingleOrDefaultAsync();

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
