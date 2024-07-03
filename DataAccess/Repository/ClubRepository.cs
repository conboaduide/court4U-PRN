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
        public async Task<IEnumerable<Club>> GetAllAsync()
        {
            using (var db = new Court4UDbContext())
            {
                return await db.Clubs.ToListAsync();
            }
        }

        public async Task<Club> GetByIdAsync(string id)
        {
            using (var db = new Court4UDbContext())
            {
                return await db.Clubs.FirstOrDefaultAsync(c => c.Id == id);
            }
        }

        public async Task AddAsync(Club club)
        {
            using (var db = new Court4UDbContext())
            {
                await db.Clubs.AddAsync(club);
                await db.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Club club)
        {
            using (var db = new Court4UDbContext())
            {
                var existingClub = await db.Clubs.FindAsync(club.Id);
                if (existingClub != null)
                {
                    existingClub.Name = club.Name;
                    existingClub.Description = club.Description;
                    existingClub.Address = club.Address;
                    existingClub.CityOfProvince = club.CityOfProvince;
                    existingClub.District = club.District;
                    existingClub.LogoUrl = club.LogoUrl;
                    existingClub.UserId = club.UserId;

                    await db.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteAsync(string id)
        {
            using (var db = new Court4UDbContext())
            {
                var club = await db.Clubs.FindAsync(id);
                if (club != null)
                {
                    db.Clubs.Remove(club);
                    await db.SaveChangesAsync();
                }
            }
        }
    }
}
