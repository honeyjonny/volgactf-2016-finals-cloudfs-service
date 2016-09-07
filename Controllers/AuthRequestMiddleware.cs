
using System;
using System.Threading.Tasks;
using CloudFs.Models;
using CloudFs.Services;
using Microsoft.AspNetCore.Http;

namespace CloudFs.Controllers
{
    public class AuthRequestMiddleware
    {
        private static string COOKIE_KEY = @"apptoken";
        private readonly RequestDelegate _next;
        private readonly IUsersRepository _usersRepo;

        public AuthRequestMiddleware (RequestDelegate next, IUsersRepository usersRepo)
        {
            _next = next;
            _usersRepo = usersRepo;
        }

        public async Task Invoke(HttpContext context)
        {
            if(context.Request.Cookies.ContainsKey(COOKIE_KEY))
            {
                var cookieValue = context.Request.Cookies[COOKIE_KEY];

                Guid userId = Guid.Parse(cookieValue);

                UserForm user;

                if(_usersRepo.GetById(userId, out user))
                {
                    context.Items["user"] = user; 
                }
            }

            await _next(context);
        }
    }
}