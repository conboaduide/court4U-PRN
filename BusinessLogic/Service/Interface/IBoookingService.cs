using DataAccess.Entity.Data;
using DataAccess.Repository.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service.Interface
{
    public interface IBookingService : IBaseService<Booking>
    {
        Task<List<Booking>> GetToDayBookingByClubId(string clubId);
        Task<List<Booking>> GetBookingInUseByClubId(string clubId);
        Task<CurrentYear[]> GetBookingInCurrentYear(string clubId);
        Task<CurrentYear[]> GetRevenueInCurrentYear(string clubId);
    }
}
