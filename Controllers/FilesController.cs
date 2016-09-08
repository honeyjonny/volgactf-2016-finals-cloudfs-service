

using System.Threading.Tasks;
using CloudFs.Models;
using CloudFs.Services;
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
        public async Task<IActionResult> AddNewFile(FileForm newFileForm)
        {
            return Ok();
        }

        [HttpGet]
        [Route("api/files/{fileId}")]
        public async Task<IActionResult> GetFileById([FromRoute] string fileId)
        {
            return Ok();
        }
    }
}