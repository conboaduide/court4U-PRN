//using DataAccess.Entity;
//using DataAccess.Entity.Data;
//using DataAccess.Repository.Interface;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace DataAccess.Repository
//{
//    public class BookedSlotRepository : IBookedSlotRepository
//    {
//        private readonly Court4UDbContext _dbContext;

//        public BookedSlotRepository(Court4UDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public async Task<BookedSlot?> Create(BookedSlot entity)
//        {
//            try
//            {
//                _dbContext.Add(entity);
//                await _dbContext.SaveChangesAsync();
//                return entity;
//            }
//            catch (Exception ex)
//            {
//                throw new Exception(ex.Message);
//            }
//        }

//        public async Task<BookedSlot?> Delete(string id)
//        {
//            try
//            {
//                var bookedSlot = await _dbContext.BookedSlots.Include(bs => bs.Slot).Where(bs => bs.Id == id).SingleOrDefaultAsync();
//                if (bookedSlot != null)
//                {
//                    _dbContext.BookedSlots.Remove(bookedSlot);
//                    await _dbContext.SaveChangesAsync();
//                }
//                return bookedSlot;
//            }
//            catch (Exception ex)
//            {
//                throw new Exception(ex.Message);
//            }
//        }

//        public async Task<BookedSlot?> Get(string id)
//        {
//            try
//            {
//                return await _dbContext.BookedSlots.Where(bs => bs.Id == id).FirstOrDefaultAsync();
//            }
//            catch (Exception ex)
//            {
//                throw new Exception(ex.Message);
//            }
//        }

//        public async Task<List<BookedSlot>> Get()
//        {
//            try
//            {
//                return await _dbContext.BookedSlots
//                    .Include(bs => bs.Slot)
//                    .ThenInclude(s => s.Club)
//                    .ToListAsync();
//            }
//            catch (Exception ex)
//            {
//                throw new Exception(ex.Message);
//            }
//        }

//        public async Task<BookedSlot?> Update(BookedSlot entity)
//        {
//            try
//            {
//                _dbContext.Entry(entity).State = EntityState.Modified;
//                await _dbContext.SaveChangesAsync();
//                return entity;
//            }
//            catch (Exception ex)
//            {
//                throw new Exception(ex.Message);
//            }
//        }

//        public async Task<List<BookedSlot>> GetByUserId(string userId)
//        {
//            try
//            {
//                return await _dbContext.BookedSlots
//                    .Where(bs => bs.Booking.UserId == userId)
//                    .Include(bs => bs.Slot)
//                    .ThenInclude(s => s.Club)
//                    .ToListAsync();
//            }
//            catch (Exception ex)
//            {
//                throw new Exception(ex.Message);
//            }
//        }

//        public async Task<BookedSlot?> GetWithBooking(string id)
//        {
//            try
//            {
//                return await _dbContext.BookedSlots
//                    .Include(bs => bs.Booking)
//                    .FirstOrDefaultAsync(bs => bs.Id == id);
//            }
//            catch (Exception ex)
//            {
//                throw new Exception(ex.Message);
//            }
//        }
//    }
//}
