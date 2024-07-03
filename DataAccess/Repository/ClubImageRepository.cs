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
    public class ClubImageRepository : IClubImageRepository
    {
        public async Task<ClubImage> Create(ClubImage ClubImage)
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    _context.Add(ClubImage);
                    _context.SaveChanges();
                    return ClubImage;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<List<ClubImage>> Get()
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    return await _context.ClubImages.ToListAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<ClubImage?> Get(string id)
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    return await _context.ClubImages.Where(x => x.Id == id).FirstOrDefaultAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<ClubImage> Update(ClubImage ClubImage)
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    _context.Update(ClubImage);
                    _context.SaveChanges();
                    return ClubImage;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<ClubImage> Delete(string id)
        {
            using (var db = new Court4UDbContext())
            {
                var found = await db.ClubImages.Where(c => c.Id == id).SingleOrDefaultAsync();

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
