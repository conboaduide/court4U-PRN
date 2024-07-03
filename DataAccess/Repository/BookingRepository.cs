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
    public class BookingRepository : IBookingRepository
    {
        public async Task<Booking> Create(Booking Booking)
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    _context.Add(Booking);
                    _context.SaveChanges();
                    return Booking;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<List<Booking>> Get()
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    return await _context.Bookings.ToListAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<Booking?> Get(string id)
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    return await _context.Bookings.Where(x => x.Id == id).FirstOrDefaultAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<Booking> Update(Booking Booking)
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    _context.Update(Booking);
                    _context.SaveChanges();
                    return Booking;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<Booking> Delete(string id)
        {
            using (var db = new Court4UDbContext())
            {
                var found = await db.Bookings.Where(c => c.Id == id).SingleOrDefaultAsync();

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
