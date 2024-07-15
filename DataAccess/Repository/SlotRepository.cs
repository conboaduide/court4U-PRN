using DataAccess.Entity.Data;
using DataAccess.Entity;
using DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public async Task<List<Slot>> GetSlotsByClubId(string clubId)
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    return await _context.Slots
                        .Where(s => s.ClubId == clubId)
                        .ToListAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        public async Task<List<Slot>> GetAvailableSlots(string slotId, DateTime startDate, DateTime endDate)
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    var bookedSlots = await _context.BookedSlots
                        .Where(bs => bs.SlotId == slotId && bs.Slot.StartTime >= startDate && bs.Slot.EndTime <= endDate)
                        .Select(bs => bs.SlotId)
                        .ToListAsync();

                    var availableSlotsQuery = _context.Slots
                        .Where(s => s.ClubId == slotId && !bookedSlots.Contains(s.Id) && s.DateOfWeek >= startDate && s.DateOfWeek <= endDate);

                    var validSlots = await availableSlotsQuery
                        .Where(slot => !_context.BookedSlots.Select(bookedSlot => bookedSlot.SlotId).Contains(slot.Id) && slot.ClubId == slotId)
                        .ToListAsync();

                    return validSlots;
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to get available slots.", ex);
                }
            }
        }

        public async Task<List<Slot>> SearchByDate(DateTime date, string ClubId)
        {
            using (var _context = new Court4UDbContext())
            {
                var validSlots = await _context.Slots
                                       .Where(slot => !_context.BookedSlots
                                                              .Select(bookedSlot => bookedSlot.SlotId)
                                                              .Contains(slot.Id) && slot.DateOfWeek == date && slot.ClubId == ClubId)
                                       .ToListAsync();
                return validSlots;
            }
        }

        public async Task Booking(Booking booking, BookedSlot bookedSlot)
        {
            using (var _context = new Court4UDbContext())
            {
                await _context.Bookings.AddAsync(booking);
                await _context.SaveChangesAsync();

                bookedSlot.BookingId = booking.Id;

                await _context.BookedSlots.AddAsync(bookedSlot);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Slot>> GetValidSlotsAsync(DateTime date, string ClubId)
        {
            using (var _context = new Court4UDbContext())
            {
                var validSlots = await _context.Slots
                                       .Where(slot => !_context.BookedSlots
                                                              .Select(bookedSlot => bookedSlot.SlotId)
                                                              .Contains(slot.Id) && slot.DateOfWeek == date && slot.ClubId == ClubId)
                                       .ToListAsync();
                return validSlots;
            }
        }
    }
}
