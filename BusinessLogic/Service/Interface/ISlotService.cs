using DataAccess.Entity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service.Interface
{
    public interface ISlotService: IBaseService<Slot>
    {
        Task<List<Slot>> GetAvailableSlotsAsync(string clubId, DateTime startDate, DateTime endDate);
        Task<bool> BookSlotAsync(string clubId, string slotId, Booking booking, BookedSlot bookedSlot);
        Task<List<Slot>> SearchByDateAsync(DateTime date, string ClubId);
    }
}
