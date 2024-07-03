using DataAccess.Entity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service.Interface
{
    public interface IUserService : IBaseService<User>
    {
        Task<User?> CheckLogin(string username, string password);
    }
}
