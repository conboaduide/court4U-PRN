using BusinessLogic.Service.Interface;
using DataAccess.Entity.Data;
using DataAccess.Repository.Interface;
using DataAccess.Repository.Request;

namespace BusinessLogic.Service
{
    public class ClubService : IClubService
    {
        private readonly IClubRepository _clubRepository;
        public ClubService(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
        }

        public async Task<IEnumerable<Club>> GetAllClubsAsync()
        {
            // Business logic can be added here if needed
            return await _clubRepository.GetAllAsync();
        }

        public async Task<Club> GetClubByIdAsync(string id)
        {
            // Business logic can be added here if needed
            return await _clubRepository.GetByIdAsync(id);
        }
        public async Task AddClubAsync(ClubRequest clubRequest)
        {
            var club = new Club()
            {
                Name = clubRequest.Name,
                Description = clubRequest.Description,
                Address = clubRequest.Address,
                CityOfProvince = clubRequest.CityOfProvince,
                District = clubRequest.District,
                LogoUrl = clubRequest.LogoUrl,
                UserId = clubRequest.UserId
            };
            // Business logic, validations, etc.
            await _clubRepository.AddAsync(club);
        }
        public async Task UpdateClubAsync(Club club)
        {
            await _clubRepository.UpdateAsync(club);
        }

        public async Task DeleteClubAsync(string id)
        {
            // Business logic, validations, etc.
            await _clubRepository.DeleteAsync(id);
        }

        public async Task<List<Club>> GetClubByUserIdAsync(string id)
        {
            var listClub = await _clubRepository.GetAllAsync();
            return listClub.Where(x => x.UserId == id).ToList();
        }
    }
}
