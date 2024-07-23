using BusinessLogic.Service.Interface;
using DataAccess.Entity.Data;
using DataAccess.Repository;
using DataAccess.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service
{
    public class SlotService : ISlotService
    {
        ISlotRepository iSlotRepository;

        public SlotService(ISlotRepository iSlotRepository)
        {
            this.iSlotRepository = iSlotRepository;
        }

        public async Task<Slot?> Create(Slot entity)
        {
            return await iSlotRepository.Create(entity);
        }

        public async Task<Slot?> Delete(string id)
        {
            return await iSlotRepository.Delete(id);
        }

        public async Task<Slot?> Get(string id)
        {
            return await iSlotRepository.Get(id);
        }

        public async Task<List<Slot>?> Get()
        {
            return await iSlotRepository.Get();
        }

        public async Task<Slot?> Update(Slot entity)
        {
            return await iSlotRepository.Update(entity);
        }
        public async Task<bool> BookSlotAsync(string clubId, string slotId, Booking booking, BookedSlot bookedSlot)
        {
            try
            {
                var slotToBook = await iSlotRepository.Get(slotId);
                if (clubId == null)
                {
                    throw new Exception("Club Id not found or does not belong to the specified club.");
                }
                if (slotToBook == null)
                {
                    throw new Exception("Slot not found or does not belong to the specified club.");
                }

                await iSlotRepository.Booking(booking, bookedSlot);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public Task<List<Slot>> GetAvailableSlotsAsync(string clubId, DateTime startDate, DateTime endDate)
        {
            return iSlotRepository.GetAvailableSlots(clubId, startDate, endDate);
        }
        public async Task<List<Slot>> SearchByDateAsync(DateTime date, string ClubId)
        {
            return await iSlotRepository.SearchByDate(date, ClubId);
        }
    }
}
