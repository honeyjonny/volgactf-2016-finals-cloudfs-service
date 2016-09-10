

using System;
using System.Security.Cryptography;
using System.Text;
using CloudFs.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CloudFs.Services
{
    public class SessionRepository : ISessionRepository
    {
        private readonly IMemoryCache _cache;

        public SessionRepository (IMemoryCache memCache)
        {
            _cache = memCache;
        }

        public void ClearSessionFor(string apptoken)
        {
            _cache.Remove(apptoken);
        }

        public bool GetUserIdBySessionCookie(string apptoken, out Guid userId)
        {
            return _cache.TryGetValue(apptoken, out userId);
        }

        public string SetSessionForUser(UserForm user)
        {
            var sha512 = SHA512.Create();

            var plaintext = string.Format("{0}+{1}+{2}+VolgaCTF{3}", 
                user.Id.ToString("N"),
                user.Username,
                user.Password,
                DateTime.Now.Year);

            var hash = sha512.ComputeHash(Encoding.UTF8.GetBytes(plaintext));

            var apptoken = BitConverter
                .ToString(hash)
                .Replace("-", "")
                .ToLower();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));

            var res = _cache.Set(apptoken, user.Id, cacheEntryOptions);

            return apptoken;
        }
    }
}