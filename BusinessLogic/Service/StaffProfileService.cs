using BusinessLogic.Service.Interface;
using DataAccess.Entity.Data;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service
{
    public class StaffProfileService : IStaffProfileService
    {
        IStaffProfileRepository iStaffProfileRepository;

        public StaffProfileService(IStaffProfileRepository iStaffProfileRepository)
        {
            this.iStaffProfileRepository = iStaffProfileRepository;
        }

        public async Task<StaffProfile?> Create(StaffProfile entity)
        {
            return await iStaffProfileRepository.Create(entity);
        }

        public async Task<StaffProfile?> Delete(string id)
        {
            return await iStaffProfileRepository.Delete(id);
        }

        public async Task<StaffProfile?> Get(string id)
        {
            return await iStaffProfileRepository.Get(id);
        }

        public async Task<List<StaffProfile>?> Get()
        {
            return await iStaffProfileRepository.Get();
        }

        public async Task<StaffProfile?> Update(StaffProfile entity)
        {
            return await iStaffProfileRepository.Update(entity);
        }
        public async Task<StaffProfile> GetWithUserAsync(string id)
        {
            return await iStaffProfileRepository.GetWithUser(id);
        }

    }
}
