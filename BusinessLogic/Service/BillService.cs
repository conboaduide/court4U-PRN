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
    public class BillService : IBillService
    {
        IBillRepository iBillRepository;

        public BillService(IBillRepository iBillRepository)
        {
            this.iBillRepository = iBillRepository;
        }

        public async Task<Bill?> Create(Bill entity)
        {
            return await iBillRepository.Create(entity);
        }

        public async Task<Bill?> Delete(string id)
        {
            return await iBillRepository.Delete(id);
        }

        public async Task<Bill?> Get(string id)
        {
            return await iBillRepository.Get(id);
        }

        public async Task<List<Bill>?> Get()
        {
            return await iBillRepository.Get();
        }

        public async Task<Bill?> Update(Bill entity)
        {
            return await iBillRepository.Update(entity);
        }
    }
}
