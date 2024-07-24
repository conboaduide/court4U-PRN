using DataAccess.Entity;
using DataAccess.Entity.Data;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ClubImageRepository : IClubImageRepository
    {
        private readonly Court4UDbContext _dbContext;

        public ClubImageRepository(Court4UDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ClubImage?> Create(ClubImage entity)
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

        public async Task<ClubImage?> Delete(string id)
        {
            try
            {
                var clubImage = await _dbContext.ClubImages.FindAsync(id);
                if (clubImage != null)
                {
                    _dbContext.ClubImages.Remove(clubImage);
                    await _dbContext.SaveChangesAsync();
                }
                return clubImage;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ClubImage?> Get(string id)
        {
            try
            {
                return await _dbContext.ClubImages.FirstOrDefaultAsync(ci => ci.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ClubImage>> Get()
        {
            try
            {
                return await _dbContext.ClubImages.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ClubImage?> Update(ClubImage entity)
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
