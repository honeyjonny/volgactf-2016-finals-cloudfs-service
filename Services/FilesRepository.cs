

using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using CloudFd.Services;
using CloudFs.Models;

namespace CloudFs.Services
{
    public class FilesRepository : IFilesRepository
    {
        private readonly AppDbContext _dbContext;

        public FilesRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddFile(FileForm newFile)
        {
            bool result = false;

            if (newFile.Id == Guid.Empty)
            {
                newFile.Id = Guid.NewGuid();
            }

            var file = 
                _dbContext
                .Files
                .SingleOrDefault(x => x.Id == newFile.Id);
            
            if(file == null
                && newFile.Filename.Length > 0
                && newFile.FolderId != Guid.Empty
                && newFile.OwnerId != Guid.Empty)
            {
                var md5 = MD5.Create();

                newFile.Checksum = md5.GetHashForString(newFile.Content);

                _dbContext.Files.Add(newFile);
                await _dbContext.SaveChangesAsync();

                result = true;
            }

            return result;
        }

        public bool GetFileById(Guid fileId, out FileForm file)
        {
            bool result = false;

            file = 
                _dbContext.Files
                .SingleOrDefault(x => x.Id == fileId);

            if(file != null)
            {
                result = true;
            }

            return result;
        }
    }
}