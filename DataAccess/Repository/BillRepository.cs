using DataAccess.Entity;
using DataAccess.Entity.Data;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class BillRepository : IBillRepository
    {
        private readonly Court4UDbContext _dbContext;

        public BillRepository(Court4UDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Bill?> Create(Bill entity)
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

        public async Task<Bill?> Delete(string id)
        {
            try
            {
                var bill = await _dbContext.Bills.FindAsync(id);
                if (bill != null)
                {
                    _dbContext.Bills.Remove(bill);
                    await _dbContext.SaveChangesAsync();
                }
                return bill;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Bill?> Get(string id)
        {
            try
            {
                return await _dbContext.Bills.FirstOrDefaultAsync(b => b.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Bill>> Get()
        {
            try
            {
                return await _dbContext.Bills.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Bill?> Update(Bill entity)
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
