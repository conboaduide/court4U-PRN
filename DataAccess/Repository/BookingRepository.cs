using DataAccess.Entity;
using DataAccess.Entity.Data;
using DataAccess.Repository.Interface;
using DataAccess.Repository.Request;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                    .Where(x => x.Slot.ClubId == clubId && x.CreatedDate.Date == today)
                    .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Booking>> GetBookingInUseByClubId(string clubId)
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
        
        public async Task<CurrentYear[]> GetBookingInCurrentYear(string clubId)
        {
            try
            {
                DateTime startOfYear = new DateTime(DateTime.Now.Year, 1, 1);
                DateTime endOfYear = new DateTime(DateTime.Now.Year, 12, 31);

                var bookings = await _dbContext.Bookings
                    .Where(x => x.Date >= startOfYear && x.Date <= endOfYear && x.Slot.ClubId == clubId)
                    .ToListAsync();

                var bookingsByMonth = bookings
                    .GroupBy(b => b.Date.Month)
                    .Select(g => new {
                        Month = g.Key,
                        Count = g.Count()
                    })
                    .OrderBy(x => x.Month)
                    .ToArray();

                CurrentYear[] result = new CurrentYear[12];
                for (int i = 1; i <= 12; i++)
                {
                    var monthData = bookingsByMonth.FirstOrDefault(x => x.Month == i);
                    result[i - 1] = new CurrentYear
                    {
                        Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i),
                        Count = monthData?.Count ?? 0
                    };
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<CurrentYear[]> GetRevenueInCurrentYear(string clubId)
        {
            try
            {
                DateTime startOfYear = new DateTime(DateTime.Now.Year, 1, 1);
                DateTime endOfYear = new DateTime(DateTime.Now.Year, 12, 31);

                var bookings = await _dbContext.Bookings
                    .Where(x => x.Date >= startOfYear && x.Date <= endOfYear && x.Slot.ClubId == clubId)
                    .ToListAsync();
                var memberSub = await _dbContext.MemberSubscriptions
                    .Where(x => x.CreatedDate >= startOfYear && x.CreatedDate <= endOfYear && x.SubscriptionOption.ClubId == clubId)
                    .ToListAsync();
                var memberSubsByMonth = memberSub
                    .GroupBy(m => m.CreatedDate.Month)
                    .Select(g => new
                    {
                        Month = g.Key,
                        TotalRevenue = (int)g.Sum(b => b.Price)
                    })
                    .OrderBy(x => x.Month)
                    .ToArray();
                var revenueByMonth = bookings
                    .GroupBy(b => b.Date.Month)
                    .Select(g => new {
                        Month = g.Key,
                        TotalRevenue = (int)g.Sum(b => b.Price) // Convert to int
                    })
                    .OrderBy(x => x.Month)
                    .ToArray();

                CurrentYear[] result = new CurrentYear[12];
                for (int i = 1; i <= 12; i++)
                {
                    var monthData = revenueByMonth.FirstOrDefault(x => x.Month == i);
                    var memberData = memberSubsByMonth.FirstOrDefault(x => x.Month == i);
                    result[i - 1] = new CurrentYear
                    {
                        Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i),
                        Count = monthData?.TotalRevenue + memberData?.TotalRevenue ?? 0
                    };
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
