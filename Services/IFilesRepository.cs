

using System;
using System.Threading.Tasks;
using CloudFs.Models;

namespace CloudFs.Services
{
    public interface IFilesRepository
    {
        Task<bool> AddFile(FileForm newFile);

        bool GetFileById(Guid fileId, out FileForm file);        
    }
}