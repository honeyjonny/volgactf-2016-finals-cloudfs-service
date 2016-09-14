using Microsoft.AspNetCore.Mvc;
using CloudFs.Models;
using CloudFs.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using CloudFS.Controllers;
using System;
using System.Linq.Expressions;

namespace CloudFs.Controllers
{
    public class UserController: Controller
    {
        private readonly IUsersRepository _usersRepo;
        private readonly IFoldersRepository _folders;

        public UserController (
            IUsersRepository repo,
            IFoldersRepository folders)
        {
          _usersRepo = repo;
          _folders = folders;
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

            if(await _usersRepo.AddUser(newUserForm))
            {
                Guid rootId = await _folders.CreateRootFolder(newUserForm.Id);

                var rootUri = string.Format("api/folders/{0}", rootId.ToString());

                result = Created(rootUri, newUserForm);
            }
            else
            {
                result = BadRequest();
            }
            return result;
        }        
    }
}