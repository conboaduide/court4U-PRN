﻿using DataAccess.Entity;
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
                _dbContext.Add(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                return await _dbContext.Bookings.ToListAsync();
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
    }
}
