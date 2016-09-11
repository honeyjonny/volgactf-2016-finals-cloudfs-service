using Microsoft.AspNetCore.Mvc;
using CloudFs.Models;
using CloudFs.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using CloudFS.Controllers;
using System;

namespace CloudFs.Controllers
{
    public class UserController: Controller
    {
        private readonly IUsersRepository _usersRepo;
        private readonly IFoldersRepository _folders;

        public UserController (
            IUsersRepository repo)
            //IFoldersRepository folders)
        {
          _usersRepo = repo;
          //_folders = folders;
        }

        [HttpGet]
        [Route("api/users")]
        public async Task<IActionResult> GetUsers()
        {
            IEnumerable<UserForm> users = _usersRepo.GetAllUsers();

            return Json(users);
        }

        [HttpPost]
        [Route("api/users")]
        public async Task<IActionResult> RegisterUser(
            [FromBody] UserForm newUserForm)
        {
            IActionResult result;

            if(_usersRepo.AddUser(newUserForm))
            {
                //Guid rootId = _folders.CreateRootFolder();

                //var rootUri = string.Format("api/folders/{0}", rootId.ToString("N"));

                result = Created("created", newUserForm);
            }
            else
            {
                result = BadRequest();
            }
            return result;
        }        
    }
}