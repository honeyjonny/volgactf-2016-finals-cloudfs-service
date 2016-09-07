

using System.Collections.Generic;
using CloudFs.Models;

namespace CloudFs.Services
{
    public interface IUsersRepository
    {
        bool AddUser(UserForm newUser);

        IEnumerable<UserForm> GetAllUsers();

    }
}