using DataAccess.Entity;
using DataAccess.Entity.Data;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        public async Task<User?> Create(User entity)
        {
            using (var db = new Court4UDbContext())
            {
                foreach (var c in db.Users.ToList())
                {
                    if (c.Username.ToLower() == entity.Username.ToLower())
                    {
                        throw new Exception("Username is existed!");
                    }
                    if (c.Email.ToLower() == entity.Email.ToLower())
                    {
                        throw new Exception("Email is existed!");
                    }
                }

                db.Add(entity);
                await db.SaveChangesAsync();
                return await db.Users.Where(c => c.Username == entity.Username).SingleOrDefaultAsync();
            }
        }

        public async Task<User?> Delete(string id)
        {
            using (var db = new Court4UDbContext())
            {
                var customer = await db.Users.Where(c => c.Id == id).SingleOrDefaultAsync();

                if (customer != null)
                {
                    db.Remove(customer);
                    await db.SaveChangesAsync();
                    return customer;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<User?> Get(string id)
        {
            using (var db = new Court4UDbContext())
            {
                return await db.Users.Where(c => c.Id == id).SingleOrDefaultAsync();
            }
        }

        public async Task<List<User>?> Get()
        {
            using (var db = new Court4UDbContext())
            {
                return await db.Users.ToListAsync();
            }
        }

        public async Task<User?> Update(User entity)
        {
            using (var db = new Court4UDbContext())
            {
                db.Update(entity);
                await db.SaveChangesAsync();
                return entity;
            }
        }
        public async Task<bool> CheckVerify(string token)
        {
            using var db = new Court4UDbContext();
            var user = await db.Users.Where(c => c.Token == token).FirstOrDefaultAsync();
            if (user == null)
            {
                return false;
            }

            user.Status = (int)Enums.Status.Active;
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<User?> GetByUsernameAndEmail(string username, string email)
        {
            using (var db = new Court4UDbContext())
            {
                var user = await db.Users
                                   .Where(u => u.Username.ToLower() == username.ToLower() &&
                                               u.Email.ToLower() == email.ToLower())
                                   .FirstOrDefaultAsync();
                return user;
            }
        }
    }
}
