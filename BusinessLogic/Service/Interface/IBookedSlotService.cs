using DataAccess.Entity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service.Interface
{
    public interface IBookedSlotService:IBaseService<BookedSlot>
    {
        Task<List<BookedSlot>?> GetByUserId(string userId);
        Task<bool> CancelBooking(string bookingId, string userId);
    }
}
