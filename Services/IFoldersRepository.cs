

using System;
using System.Collections.Generic;
using CloudFs.Models;

namespace CloudFs.Services
{
    public interface IFoldersRepository
    {
        Guid CreateRootFolder(Guid ownerId);

        bool AddFolder(FolderForm newFolder);

        bool GetFolderById(Guid folderId, out FolderForm folder);

        IEnumerable<FolderForm> GetChildsFolders(Guid parentId);

        IEnumerable<FileForm> GetFilesInFolder(Guid folderId);        
    }
}