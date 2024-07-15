using DataAccess.Entity.Data;
using DataAccess.Repository.Request;

namespace BusinessLogic.Service.Interface
{
    public interface IClubService
    {
        Task<IEnumerable<Club>> GetAllClubsAsync();
        Task<Club> GetClubByIdAsync(string id);
        Task<Club> GetClubByUserIdAsync(string id);
        Task AddClubAsync(ClubRequest clubRequest);
        Task UpdateClubAsync(Club club);
        Task DeleteClubAsync(string id);
    }
}
