using BusinessLogic.Service.Interface;
using DataAccess.Entity.Data;
using DataAccess.Entity;
using DataAccess.Repository;
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
        public async Task<User?> CheckLogin(string identifier, string password)
        {
            var users = await iuserRepository.Get();
            if (users != null)
            {
                var user = users.FirstOrDefault(c => (c.Username.ToLower().Equals(identifier.ToLower()) ||
                                             c.Email.ToLower().Equals(identifier.ToLower()) &&
                                            c.Password!.Equals(password)) &&
                                            c.Status != Enums.Status.Inactive);
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
        public async Task<bool> CheckVerify(string token)
        {
            return await iuserRepository.CheckVerify(token);
        }
        public async Task<User?> GetByUsernameAndEmail(string username, string email)
        {
            return await iuserRepository.GetByUsernameAndEmail(username, email);
        }
    }
}
