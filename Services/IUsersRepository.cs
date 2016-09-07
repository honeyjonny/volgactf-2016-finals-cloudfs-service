

using System;
using System.Collections.Generic;
using CloudFs.Models;

namespace CloudFs.Services
{
    public interface IUsersRepository
    {
        bool AddUser(UserForm newUser);

        bool GetById(Guid id, out UserForm user);

        IEnumerable<UserForm> GetAllUsers();

    }
}