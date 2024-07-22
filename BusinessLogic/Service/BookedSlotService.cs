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
    public class BookedSlotService : IBookedSlotService
    {
        IBookedSlotRepository iBookedSlotRepository;

        public BookedSlotService(IBookedSlotRepository iBookedSlotRepository)
        {
            this.iBookedSlotRepository = iBookedSlotRepository;
        }

        public async Task<BookedSlot?> Create(BookedSlot entity)
        {
            return await iBookedSlotRepository.Create(entity);
        }

        public async Task<BookedSlot?> Delete(string id)
        {
            return await iBookedSlotRepository.Delete(id);
        }

        public async Task<BookedSlot?> Get(string id)
        {
            return await iBookedSlotRepository.Get(id);
        }

        public async Task<List<BookedSlot>?> Get()
        {
            return await iBookedSlotRepository.Get();
        }

        public async Task<BookedSlot?> Update(BookedSlot entity)
        {
            return await iBookedSlotRepository.Update(entity);
        }
    }
}
