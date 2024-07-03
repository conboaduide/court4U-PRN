using DataAccess.Entity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interface
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<bool> CheckVerify(string token);
        Task<User?> GetByUsernameAndEmail(string username, string email);
    }
}
