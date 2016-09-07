

using System;
using System.Collections.Generic;
using CloudFs.Models;

namespace CloudFs.Services
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IDictionary<Guid, UserForm> _allUsers;

        public UsersRepository ()
        {
          _allUsers = new Dictionary<Guid, UserForm>();
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