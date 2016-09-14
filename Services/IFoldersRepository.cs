

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudFs.Models;

namespace CloudFs.Services
{
    public interface IFoldersRepository
    {
        Task<Guid> CreateRootFolder(Guid ownerId);

        Task<bool> AddFolder(FolderForm newFolder);

        bool GetFolderById(Guid folderId, out FolderForm folder);

        IEnumerable<FolderForm> GetChildsFolders(Guid parentId);

        IEnumerable<FileForm> GetFilesInFolder(Guid folderId);        
    }
}