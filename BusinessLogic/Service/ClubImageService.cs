using BusinessLogic.Service.Interface;
using DataAccess.Entity.Data;
using DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service
{
    public class ClubImageService : IClubImageService
    {
        IClubImageRepository iClubImageRepository;

        public ClubImageService(IClubImageRepository iClubImageRepository)
        {
            this.iClubImageRepository = iClubImageRepository;
        }

        public async Task<ClubImage?> Create(ClubImage entity)
        {
            return await iClubImageRepository.Create(entity);
        }

        public async Task<ClubImage?> Delete(string id)
        {
            return await iClubImageRepository.Delete(id);
        }

        public async Task<ClubImage?> Get(string id)
        {
            return await iClubImageRepository.Get(id);
        }

        public async Task<List<ClubImage>?> Get()
        {
            return await iClubImageRepository.Get();
        }

        public async Task<ClubImage?> Update(ClubImage entity)
        {
            return await iClubImageRepository.Update(entity);
        }
    }
}
