using DataAccess.Entity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interface
{
    public interface IBookedSlotRepository : IBaseRepository<BookedSlot>
    {
        Task<List<BookedSlot>> GetByUserId(string userId);
        Task<BookedSlot> GetWithBooking(string id);
    }
}
