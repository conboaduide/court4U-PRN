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
        public async Task<List<Slot>> GetAvailableSlotsAsync(string clubId, DateTime startDate, DateTime endDate, DateTime searchDate)
        {
            return await iSlotRepository.GetAvailableSlots(clubId, startDate, endDate , searchDate);
        }
        public async Task<List<Slot>> SearchByDateAsync(DateTime date, string ClubId)
        {
            return await iSlotRepository.SearchByDate(date, ClubId);
        }

        public async Task<Booking> Booking(Booking booking, Bill bill)
        {
            return await iSlotRepository.Booking(booking, bill);
        }

        public async Task<List<Slot>> GetSlotsByClubId(string id)
        {
            return await iSlotRepository.GetSlotsByClubId(id);
        }
    }
}
