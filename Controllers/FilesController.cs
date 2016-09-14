

using System;
using System.Threading.Tasks;
using CloudFs.Models;
using CloudFs.Services;
using CloudFS.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CloudFs.Controllers
{
    public class FilesController : Controller
    {
        private readonly IFilesRepository _filesRepo;

        public FilesController (IFilesRepository filesRepo)
        {
          _filesRepo = filesRepo;
        }

        [HttpPost]
        [Route("api/files")]
        public async Task<IActionResult> AddNewFile(
            [FromBody] FileForm newFileForm)
        {
            IActionResult result;
            UserForm user;

            if(Request.GetUser(out user))
            {
                newFileForm.OwnerId = user.Id;

                if(await _filesRepo.AddFile(newFileForm))
                {
                    var uri = string.Format("api/files/{0}", newFileForm.Id.ToString());
                    result = Created(uri, newFileForm);
                }
                else
                {
                    result = NoContent();
                }
            }
            else
            {
                result = BadRequest();
            }

            return result;
        }

        [HttpGet]
        [Route("api/files/{fileId}")]
        public async Task<IActionResult> GetFileById(
            [FromRoute] Guid fileId,
            [FromQuery] string checksum)
        {
            IActionResult result;
            UserForm user;

            if(Request.GetUser(out user))
            {
                FileForm file;

                if(_filesRepo.GetFileById(fileId, out file)
                    && file.Checksum.Equals(checksum)
                    && file.OwnerId == user.Id)
                {
                    result = Ok(file);
                }
                else
                {
                    result = Forbid();
                }
            }
            else
            {
                result = BadRequest();
            }

            return result;
        }
    }
}