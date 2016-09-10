

using System;
using CloudFs.Models;

namespace CloudFs.Services
{
    public interface ISessionRepository
    {
        string SetSessionForUser(UserForm user);

        bool GetUserIdBySessionCookie(string apptoken, out Guid userId);

        void ClearSessionFor(string apptoken);
    }
}