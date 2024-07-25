using DataAccess.Entity;
using DataAccess.Entity.Data;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class StaffProfileRepository : IStaffProfileRepository
    {
        private readonly Court4UDbContext _dbContext;

        public StaffProfileRepository(Court4UDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<StaffProfile?> Create(StaffProfile entity)
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

        public async Task<StaffProfile?> Delete(string id)
        {
            try
            {
                var staffProfile = await _dbContext.StaffProfiles.FindAsync(id);
                if (staffProfile != null)
                {
                    _dbContext.StaffProfiles.Remove(staffProfile);
                    await _dbContext.SaveChangesAsync();
                }
                return staffProfile;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<StaffProfile?> Get(string id)
        {
            try
            {
                return await _dbContext.StaffProfiles.FirstOrDefaultAsync(sp => sp.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<StaffProfile>> Get()
        {
            try
            {
                return await _dbContext.StaffProfiles.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<StaffProfile?> Update(StaffProfile entity)
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
        public async Task<StaffProfile> GetWithUser(string id)
        {
            return await _dbContext.StaffProfiles
                .Include(sp => sp.User)
                .FirstOrDefaultAsync(sp => sp.Id == id);
        }
    }
}
