using DataAccess.Entity;
using DataAccess.Entity.Data;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly Court4UDbContext _dbContext;

        public BookingRepository(Court4UDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Booking?> Create(Booking entity)
        {
            try
            {
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {

                var innerExceptionMessage = ex.InnerException?.Message ?? "No inner exception";
                var errorMessage = $"Error: {ex.Message}, Inner Exception: {innerExceptionMessage}";
                Console.WriteLine(errorMessage);
                throw new Exception(errorMessage);
            }
        }

        public async Task<Booking?> Delete(string id)
        {
            try
            {
                var booking = await _dbContext.Bookings.FindAsync(id);
                if (booking != null)
                {
                    _dbContext.Bookings.Remove(booking);
                    await _dbContext.SaveChangesAsync();
                }
                return booking;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Booking?> Get(string id)
        {
            try
            {
                return await _dbContext.Bookings.FirstOrDefaultAsync(b => b.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Booking>> Get()
        {
            try
            {
                return await _dbContext.Bookings.Include(x => x.Slot.Club).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Booking?> Update(Booking entity)
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

        public async Task<List<Booking>> GetToDayBookingByClubId(string clubId)
        {
            try
            {
                DateTime today = DateTime.Now.Date;
                var result = await _dbContext.Bookings
                    .Where(x => x.Slot.ClubId == clubId && x.Date.Date == today)
                    .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
