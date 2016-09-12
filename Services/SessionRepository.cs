

using System;
using System.Security.Cryptography;
using System.Text;
using CloudFd.Services;
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

            var plaintext = string.Format("{0}+VolgaCTF{1}:{2}", 
                user.Id.ToString("N"),
                DateTime.Now.Year,
                DateTime.Now.Hour);

            var apptoken = sha512.GetHashForString(plaintext);

            var cacheEntryOptions = 
                new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));

            var res = _cache.Set(apptoken, user.Id, cacheEntryOptions);

            return apptoken;
        }
    }
}