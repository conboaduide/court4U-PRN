using DataAccess.Entity.Data;

namespace DataAccess.Repository.Interface
{
    public interface IClubRepository
    {
        Task<IEnumerable<Club>> GetAllAsync();
        Task<Club> GetByIdAsync(string id);
        Task AddAsync(Club club);
        Task UpdateAsync(Club club);
        Task DeleteAsync(string id);
    }
}
