

using System;
using System.Collections.Generic;
using CloudFs.Models;

namespace CloudFs.Services
{
    public interface IFoldersRepository
    {
        Guid CreateRootForUser(UserForm userForm);

        bool AddFolder(FolderForm newFolder);

        IEnumerable<FolderForm> GetChildsFolders(FolderForm parentFolder);        
    }
}