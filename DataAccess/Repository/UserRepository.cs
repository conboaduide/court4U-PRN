using DataAccess.Entity;
using DataAccess.Entity.Data;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly Court4UDbContext _dbContext;

        public UserRepository(Court4UDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> Create(User entity)
        {
            // Check for existing username or email
            if (await _dbContext.Users.AnyAsync(u => u.Username.ToLower() == entity.Username.ToLower()))
            {
                throw new Exception("Username is already taken.");
            }

            if (await _dbContext.Users.AnyAsync(u => u.Email.ToLower() == entity.Email.ToLower()))
            {
                throw new Exception("Email is already registered.");
            }

            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<User?> Delete(string id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
            return user;
        }

        public async Task<User?> Get(string id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<User>> Get()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User?> Update(User entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> CheckVerify(string token)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Token == token);
            if (user != null)
            {
                user.Status = (int)Enums.Status.Active;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<User?> GetByUsernameAndEmail(string username, string email)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower() && u.Email.ToLower() == email.ToLower());
        }
    }
}
