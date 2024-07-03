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
    public class UserService : IUserService
    {
        IUserRepository iuserRepository;

        public UserService(IUserRepository iuserRepository)
        {
            this.iuserRepository = iuserRepository;
        }
        public async Task<User?> CheckLogin(string username, string password)
        {
            var users = await iuserRepository.Get();
            if (users != null)
            {
                var user = users.Where(c => c.Username.ToLower().Equals(username.ToLower()) && c.Password!.Equals(password)).SingleOrDefault();
                
                return user;
            }
            else return null;
        }

            public async Task<User?> Create(User entity)
        {
            return await iuserRepository.Create(entity);
        }

        public async Task<User?> Delete(string id)
        {
            return await iuserRepository.Delete(id);
        }

        public async Task<User?> Get(string id)
        {
            return await iuserRepository.Get(id); 
        }

        public async Task<List<User>?> Get()
        {
           return await iuserRepository.Get();
        }

        public async Task<User?> Update(User entity)
        {
            return await iuserRepository.Update(entity);
        }
    }
}
