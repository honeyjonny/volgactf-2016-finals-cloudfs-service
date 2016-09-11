

using System;
using System.Collections.Generic;
using CloudFs.Models;

namespace CloudFs.Services
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IDictionary<Guid, UserForm> _allUsers;

        private readonly AppDbContext _dbcontext;

        public UsersRepository (AppDbContext dbContext)
        {
            _allUsers = new Dictionary<Guid, UserForm>();
            _dbcontext = dbContext;
        }

        public bool AddUser(UserForm newUser)
        {
            bool result = false;

            if(newUser.Id == Guid.Empty)
            {
                newUser.Id = Guid.NewGuid();
            }

            if(!_allUsers.ContainsKey(newUser.Id))
            {
                _allUsers.Add(newUser.Id, newUser);

                _dbcontext.Add(newUser);
                _dbcontext.SaveChanges();

                result = true;
            }

            return result;
        }

        public IEnumerable<UserForm> GetAllUsers()
        {
            return _allUsers.Values;
        }

        public bool GetById(Guid id, out UserForm user)
        {
            bool result = false;
            user = null;

            if(_allUsers.ContainsKey(id))
            {
                result = true;

                user = _allUsers[id];
            }            

            return result;
        }
    }
}