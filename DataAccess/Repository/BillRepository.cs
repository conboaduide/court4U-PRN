using DataAccess.Entity.Data;
using DataAccess.Entity;
using DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class BillRepository : IBillRepository
    {
        public async Task<Bill> Create(Bill bill)
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    _context.Add(bill);
                    _context.SaveChanges();
                    return bill;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<List<Bill>> Get()
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    return await _context.Bills.ToListAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<Bill?> Get(string id)
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    return await _context.Bills.Where(x => x.Id == id).FirstOrDefaultAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<Bill> Update(Bill bill)
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    _context.Update(bill);
                    _context.SaveChanges();
                    return bill;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<Bill> Delete(string id)
        {
            using (var db = new Court4UDbContext())
            {
                var found = await db.Bills.Where(c => c.Id == id).SingleOrDefaultAsync();

                if (found != null)
                {
                    db.Remove(found);
                    await db.SaveChangesAsync();
                    return found;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
