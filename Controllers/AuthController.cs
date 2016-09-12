

using System;
using System.Threading.Tasks;
using CloudFs.Models;
using CloudFs.Services;
using CloudFS.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CloudFs.Controllers
{
    public class AuthController : Controller
    {
        private readonly ISessionRepository _sessions;

        private readonly IUsersRepository _users;
        public AuthController(
            ISessionRepository sessions,
            IUsersRepository usersRepo)
        {
            _sessions = sessions;
            _users = usersRepo;
        }

        [HttpPost]
        [Route("/api/auth/{userId}/login")]
        public async Task<IActionResult> LoginHandler([FromRoute] Guid userId, UserForm userForm)
        {
            IActionResult result;

            if (userId == Guid.Empty)
            {
                result = BadRequest();
            }
            else
            {
                UserForm user;

                if (_users.GetById(userId, out user)
                    && userForm.Username.Equals(user.Username)
                    && userForm.Password.Equals(user.Password))
                {
                    var apptokenValue = _sessions.SetSessionForUser(user);

                    var apptoken = string.Format("{0}={1}", Consts.COOKIE_KEY, apptokenValue);

                    Response.Headers.Add("Set-Cookie", apptoken);

                    result = Ok();
                }
                else
                {
                    result = NotFound();
                }
            }

            return result;
        }

        [HttpPost]
        [Route("/api/auth/{userId}/logout")]
        public async Task<IActionResult> LogoutHandler([FromRoute] Guid userId)
        {
            IActionResult result;
            UserForm user;
            string cookie;

            if (Request.GetUser(out user) 
                && Request.GetCookie(out cookie))
            {
                _sessions.ClearSessionFor(cookie);
                result = Ok();
            }
            else
            {
                result = NotFound();
            }

            return result;
        }

    }

}