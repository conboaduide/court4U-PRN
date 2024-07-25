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
    public class BookingService : IBookingService
    {
        IBookingRepository iBookingRepository;

        public BookingService(IBookingRepository iBookingRepository)
        {
            this.iBookingRepository = iBookingRepository;
        }

        public async Task<Booking?> Create(Booking entity)
        {
            return await iBookingRepository.Create(entity);
        }

        public async Task<Booking?> Delete(string id)
        {
            return await iBookingRepository.Delete(id);
        }

        public async Task<Booking?> Get(string id)
        {
            return await iBookingRepository.Get(id);
        }

        public async Task<List<Booking>?> Get()
        {
            return await iBookingRepository.Get();
        }

        public async Task<Booking?> Update(Booking entity)
        {
            return await iBookingRepository.Update(entity);
        }
        public async Task<List<Booking>> GetToDayBookingByClubId(string clubId)
        {
            return await iBookingRepository.GetToDayBookingByClubId(clubId);
        }
    }
}
