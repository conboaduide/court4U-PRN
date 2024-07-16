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
    public class CourtRepository : ICourtRepository
    {
        public async Task<Court> Create(Court Court)
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    _context.Add(Court);
                    _context.SaveChanges();
                    return Court;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<List<Court>> Get()
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    return await _context.Courts
                        .Include(c => c.Club)
                        .ToListAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<Court?> Get(string id)
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    return await _context.Courts.Where(x => x.Id == id).FirstOrDefaultAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<Court> Update(Court Court)
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    _context.Update(Court);
                    _context.SaveChanges();
                    return Court;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<Court> Delete(string id)
        {
            using (var db = new Court4UDbContext())
            {
                var found = await db.Courts.Where(c => c.Id == id).SingleOrDefaultAsync();

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

        public async Task<List<Court>> GetCourtsByClubId(string clubId)
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    return await _context.Courts
                        .Where(c => c.ClubId == clubId)
                        .ToListAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
