using DataAccess.Entity;
using DataAccess.Entity.Data;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly Court4UDbContext _dbContext;

        public ReviewRepository(Court4UDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Review?> Create(Review entity)
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

        public async Task<Review?> Delete(string id)
        {
            try
            {
                var review = await _dbContext.Reviews.FindAsync(id);
                if (review != null)
                {
                    _dbContext.Reviews.Remove(review);
                    await _dbContext.SaveChangesAsync();
                }
                return review;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Review?> Get(string id)
        {
            try
            {
                return await _dbContext.Reviews.FirstOrDefaultAsync(r => r.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Review>> Get()
        {
            try
            {
                return await _dbContext.Reviews.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Review?> Update(Review entity)
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
