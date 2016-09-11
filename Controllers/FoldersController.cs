
using System;
using System.Threading.Tasks;
using CloudFs.Models;
using CloudFs.Services;
using CloudFS.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CloudFs.Controllers
{
    public class FoldersController : Controller
    {
        private readonly IFoldersRepository _foldersRepo;

        public FoldersController(IFoldersRepository foldersRepo)
        {
            _foldersRepo = foldersRepo;
        }

        [HttpPost]
        [Route("/api/folders")]
        public async Task<IActionResult> CreateNewFolder(
            [FromBody] FolderForm newFolderForm)
        {
            IActionResult result;
            UserForm user;

            if(Request.GetUser(out user))
            {
                if(_foldersRepo.AddFolder(newFolderForm))
                {
                    var uri = string.Format("api/folders/{0}", newFolderForm.Id.ToString("N"));
                    result = Created(uri, newFolderForm);
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
        [Route("/api/folders/{folderId}")]
        public async Task<IActionResult> GetFolder(
            [FromRoute] Guid folderId)
        {
            IActionResult result;
            UserForm user;

            if (Request.GetUser(out user))
            {
                FolderForm folder;

                if (_foldersRepo.GetFolderById(folderId, out folder))
                {
                    result = Ok(folder);
                }
                else
                {
                    result = NotFound(folderId);
                }
            }
            else
            {
                result = BadRequest();
            }

            return result;
        }


        [HttpGet]
        [Route("api/folders/{folderId}/subfolders")]
        public async Task<IActionResult> GetSubFolders(
            [FromRoute] Guid folderId)
        {
            IActionResult result;
            UserForm user;

            if(Request.GetUser(out user))
            {
                var folders = _foldersRepo.GetChildsFolders(folderId);

                result = Ok(folders);
            }
            else
            {
                result  = BadRequest();
            }

            return result;
        }


        [HttpGet]
        [Route("api/folders/{folderId}/files")]
        public async Task<IActionResult> GetFilesInFolder(
            [FromRoute] Guid folderId)
        {
            IActionResult result;
            UserForm user;

            if(Request.GetUser(out user))
            {
                var files = _foldersRepo.GetFilesInFolder(folderId);

                result = Ok(files);
            }
            else
            {
                result  = BadRequest();
            }

            return result;            
        }
    }
}