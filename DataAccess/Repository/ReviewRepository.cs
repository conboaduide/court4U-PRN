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
    public class ReviewRepository : IReviewRepository
    {
        public async Task<Review> Create(Review Review)
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    _context.Add(Review);
                    _context.SaveChanges();
                    return Review;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<List<Review>> Get()
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    return await _context.Reviews.ToListAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<Review?> Get(string id)
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    return await _context.Reviews.Where(x => x.Id == id).FirstOrDefaultAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<Review> Update(Review Review)
        {
            using (var _context = new Court4UDbContext())
            {
                try
                {
                    _context.Update(Review);
                    _context.SaveChanges();
                    return Review;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<Review> Delete(string id)
        {
            using (var db = new Court4UDbContext())
            {
                var found = await db.Reviews.Where(c => c.Id == id).SingleOrDefaultAsync();

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
