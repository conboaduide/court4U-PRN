using BusinessLogic.Service.Interface;
using DataAccess.Entity.Data;
using DataAccess.Repository;
using DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service
{
    public class CourtService : ICourtService
    {
        ICourtRepository iCourtRepository;

        public CourtService(ICourtRepository iCourtRepository)
        {
            this.iCourtRepository = iCourtRepository;
        }

        public async Task<Court?> Create(Court entity)
        {
            return await iCourtRepository.Create(entity);
        }

        public async Task<Court?> Delete(string id)
        {
            return await iCourtRepository.Delete(id);
        }

        public async Task<Court?> Get(string id)
        {
            return await iCourtRepository.Get(id);
        }

        public async Task<List<Court>?> Get()
        {
            return await iCourtRepository.Get();
        }

        public async Task<Court?> Update(Court entity)
        {
            return await iCourtRepository.Update(entity);
        }
        public async Task<List<Court>> GetCourtsByClubIdAsync(string clubId)
        {
            return await iCourtRepository.GetCourtsByClubId(clubId);
        }
    }
}
