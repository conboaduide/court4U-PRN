using DataAccess.Entity;
using DataAccess.Entity.Data;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CourtRepository : ICourtRepository
    {
        private readonly Court4UDbContext _dbContext;

        public CourtRepository(Court4UDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Court?> Create(Court entity)
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

        public async Task<Court?> Delete(string id)
        {
            try
            {
                var court = await _dbContext.Courts.FindAsync(id);
                if (court != null)
                {
                    _dbContext.Courts.Remove(court);
                    await _dbContext.SaveChangesAsync();
                }
                return court;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Court?> Get(string id)
        {
            try
            {
                return await _dbContext.Courts.Include(c => c.Club).FirstOrDefaultAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Court>> Get()
        {
            try
            {
                return await _dbContext.Courts.Include(c => c.Club).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Court?> Update(Court entity)
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

        public async Task<List<Court>> GetCourtsByClubId(string clubId)
        {
            try
            {
                return await _dbContext.Courts.Include(c => c.Club).Where(c => c.ClubId == clubId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
