

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudFs.Models;

namespace CloudFs.Services
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AppDbContext _dbcontext;

        public UsersRepository (AppDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<bool> AddUser(UserForm newUser)
        {
            bool result = false;

            if(newUser.Id == Guid.Empty)
            {
                newUser.Id = Guid.NewGuid();
            }

            var user = _dbcontext
                .Users
                .SingleOrDefault(x => x.Id == newUser.Id);

            if(user == null)
            {
                _dbcontext.Add(newUser);
                await _dbcontext.SaveChangesAsync();

                result = true;
            }

            return result;
        }

        public IEnumerable<UserForm> GetAllUsers()
        {
            return _dbcontext
                .Users
                .Select(x => new UserForm
                    {
                        Id = x.Id,
                        Username = x.Username
                    });
        }

        public bool GetById(Guid id, out UserForm user)
        {
            bool result = false;

            user = _dbcontext
                .Users
                .SingleOrDefault(x => x.Id == id);      

            if(user != null)
            {
                result = true;
            }      

            return result;
        }
    }
}