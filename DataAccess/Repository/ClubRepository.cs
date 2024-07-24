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
        private readonly Court4UDbContext _context;

        public ClubRepository(Court4UDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Club>> GetAllAsync()
        {
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
            return await _context.Clubs.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Club club)
        {
            await _context.Clubs.AddAsync(club);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Club club)
        {
            var existingClub = await _context.Clubs.FindAsync(club.Id);
            if (existingClub != null)
            {
                existingClub.Name = club.Name;
                existingClub.Description = club.Description;
                existingClub.Address = club.Address;
                existingClub.CityOfProvince = club.CityOfProvince;
                existingClub.District = club.District;
                existingClub.LogoUrl = club.LogoUrl;
                existingClub.UserId = club.UserId;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(string id)
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