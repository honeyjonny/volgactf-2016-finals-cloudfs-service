

using System;
using CloudFs.Models;

namespace CloudFs.Services
{
    public interface IFilesRepository
    {
        bool AddFile(FileForm newFile);

        bool GetFileById(Guid fileId, out FileForm file);        
    }
}