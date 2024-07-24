using DataAccess.Entity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interface
{
    public interface ISlotRepository : IBaseRepository<Slot>
    {
        Task<List<Slot>> GetSlotsByClubId(string clubId);
        //Task<List<Slot>> GetAvailableSlots(string clubId, DateTime startDate, DateTime endDate);
        //Task<List<Slot>> SearchByDate(DateTime date, string ClubId);
        //Task Booking(Booking booking, BookedSlot bookedSlot);
        //Task<List<Slot>> GetValidSlotsAsync(DateTime date, string ClubId);
    }
}
