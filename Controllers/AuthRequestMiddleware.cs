
using System;
using System.Threading.Tasks;
using CloudFs.Models;
using CloudFs.Services;
using CloudFS.Controllers;
using Microsoft.AspNetCore.Http;

namespace CloudFs.Controllers
{
    public class AuthRequestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUsersRepository _usersRepo;
        private readonly ISessionRepository _sessions;

        public AuthRequestMiddleware (
            RequestDelegate next, 
            IUsersRepository usersRepo,
            ISessionRepository sessionsRepo)
        {
            _next = next;
            _usersRepo = usersRepo;
            _sessions = sessionsRepo;
        }

        public async Task Invoke(HttpContext context)
        {
            string cookieValue;

            if(context.Request.GetCookie(out cookieValue))
            {
                Guid userId;
                UserForm user;

                if(_sessions.GetUserIdBySessionCookie(cookieValue, out userId) 
                    && _usersRepo.GetById(userId, out user))
                {
                    context.Items[Consts.USER_ITEM] = user; 
                }
            }

            await _next(context);
        }
    }
}