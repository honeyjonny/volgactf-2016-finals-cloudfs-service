
using System.Threading.Tasks;
using CloudFs.Models;
using CloudFs.Services;
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
        public async Task<IActionResult> PostNewFolder(FolderForm newFolderForm)
        {
            return Ok();
        }

        [HttpGet]
        [Route("/api/folders")]
        public async Task<IActionResult> GetFoldersTreeForUser()
        {
            return Ok();
        }

    }
}