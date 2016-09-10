using Microsoft.AspNetCore.Mvc;
using CloudFs.Models;
using CloudFs.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using CloudFS.Controllers;

namespace CloudFs.Controllers
{
    public class UserController: Controller
    {
        private readonly IUsersRepository _usersRepo;

        public UserController (IUsersRepository repo)
        {
          _usersRepo = repo;
        }

        [HttpGet]
        [Route("api/users")]
        public async Task<IActionResult> GetUsers()
        {
            IEnumerable<UserForm> users = _usersRepo.GetAllUsers();

            return Json(users);
        }


        [HttpGet]
        [RouteAttribute("/test")]
        public async Task<IActionResult> GetHome()
        {
            UserForm user;

            if(Request.GetUser(out user))
            {
                var v = string.Format("Hello {0}", user.Username);
                return Json(v);
            }
            else
            {
                return Json("not user");
            }
        }

        [HttpPost]
        [Route("api/users")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForm newUserForm)
        {
            IActionResult result;

            bool success = _usersRepo.AddUser(newUserForm);

            if(success)
            {
                result = Created(newUserForm.Id.ToString(), newUserForm);
            }
            else
            {
                result = BadRequest();
            }
            return result;
        }        
    }
}