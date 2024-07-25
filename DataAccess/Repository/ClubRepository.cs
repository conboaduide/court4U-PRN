using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Entity;
using DataAccess.Entity.Data;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class ClubRepository : IClubRepository
    {
        public ClubRepository()
        {
        }

        public async Task<IEnumerable<Club>> GetAllAsync()
        {
            using (var _context = new Court4UDbContext())
                try
                {
                    return await _context.Clubs.ToListAsync();
                }
                catch (Exception ex)
                {
                    // Log lỗi chi tiết
                    Console.WriteLine($"Error in GetAllAsync: {ex.Message}");
                    throw;
                }
        }

        public async Task<Club> GetByIdAsync(string id)
        {
            using (var _context = new Court4UDbContext())
                return await _context.Clubs.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Club club)
        {
            using (var _context = new Court4UDbContext())
            {
                await _context.Clubs.AddAsync(club);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Club club)
        {
            using (var _context = new Court4UDbContext())
            {
                var existingClub = await _context.Clubs.Include(c => c.User).Where(c => c.Id == club.Id).SingleOrDefaultAsync();
                if (existingClub != null)
                {
                    existingClub.Name = club.Name;
                    existingClub.Description = club.Description;
                    existingClub.Address = club.Address;
                    existingClub.CityOfProvince = club.CityOfProvince;
                    existingClub.District = club.District;
                    existingClub.LogoUrl = club.LogoUrl;
                    existingClub.UserId = club.UserId;
                    existingClub.User.Id = club.UserId;

                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteAsync(string id)
        {
            using (var _context = new Court4UDbContext())
            {
                var club = await _context.Clubs.FindAsync(id);
                if (club != null)
                {
                    _context.Clubs.Remove(club);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}