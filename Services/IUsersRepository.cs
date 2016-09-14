

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudFs.Models;

namespace CloudFs.Services
{
    public interface IUsersRepository
    {
        Task<bool> AddUser(UserForm newUser);

        bool GetById(Guid id, out UserForm user);

        IEnumerable<UserForm> GetAllUsers();

    }
}