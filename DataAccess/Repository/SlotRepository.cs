using DataAccess.Entity;
using DataAccess.Entity.Data;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class SlotRepository : ISlotRepository
    {
        private readonly Court4UDbContext _dbContext;

        public SlotRepository(Court4UDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Slot?> Create(Slot entity)
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

        public async Task<Slot?> Delete(string id)
        {
            try
            {
                var slot = await _dbContext.Slots.FindAsync(id);
                if (slot != null)
                {
                    _dbContext.Slots.Remove(slot);
                    await _dbContext.SaveChangesAsync();
                }
                return slot;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Slot?> Get(string id)
        {
            try
            {
                return await _dbContext.Slots.FirstOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Slot>> Get()
        {
            try
            {
                return await _dbContext.Slots.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Slot?> Update(Slot entity)
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

        public async Task<List<Slot>> GetSlotsByClubId(string clubId)
        {
            try
            {
                return await _dbContext.Slots.Where(s => s.ClubId == clubId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //public async Task<List<Slot>> GetAvailableSlots(string slotId, DateTime startDate, DateTime endDate)
        //{
        //    try
        //    {
        //        var bookedSlots = await _dbContext.BookedSlots
        //            .Where(bs => bs.SlotId == slotId && bs.Slot.StartTime >= startDate && bs.Slot.EndTime <= endDate)
        //            .Select(bs => bs.SlotId)
        //            .ToListAsync();

        //        var availableSlots = await _dbContext.Slots
        //            .Where(s => s.ClubId == slotId && !bookedSlots.Contains(s.Id) && s.DateOfWeek >= startDate && s.DateOfWeek <= endDate)
        //            .ToListAsync();

        //        return availableSlots;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Failed to get available slots.", ex);
        //    }
        //}

        //public async Task<List<Slot>> SearchByDate(DateTime date, string clubId)
        //{
        //    try
        //    {
        //        var validSlots = await _dbContext.Slots
        //            .Where(slot => !_dbContext.BookedSlots.Select(bookedSlot => bookedSlot.SlotId).Contains(slot.Id) && slot.DateOfWeek == date && slot.ClubId == clubId)
        //            .ToListAsync();
        //        return validSlots;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //public async Task Booking(Booking booking, BookedSlot bookedSlot)
        //{
        //    try
        //    {
        //        await _dbContext.Bookings.AddAsync(booking);
        //        await _dbContext.SaveChangesAsync();

        //        bookedSlot.BookingId = booking.Id;

        //        await _dbContext.BookedSlots.AddAsync(bookedSlot);
        //        await _dbContext.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //public async Task<List<Slot>> GetValidSlotsAsync(DateTime date, string clubId)
        //{
        //    try
        //    {
        //        var validSlots = await _dbContext.Slots
        //            .Where(slot => !_dbContext.BookedSlots.Select(bookedSlot => bookedSlot.SlotId).Contains(slot.Id) && slot.DateOfWeek == date && slot.ClubId == clubId)
        //            .ToListAsync();
        //        return validSlots;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
    }
}
