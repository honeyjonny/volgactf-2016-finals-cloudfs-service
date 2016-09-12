

using System;
using System.Collections.Generic;
using System.Linq;
using CloudFs.Models;

namespace CloudFs.Services
{
    public class FoldersRepository : IFoldersRepository
    {
        private readonly AppDbContext _dbContext;

        public FoldersRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddFolder(FolderForm newFolder)
        {
            bool result = false;

            if (newFolder.Id == Guid.Empty)
            {
                newFolder.Id = Guid.NewGuid();
            }

            var folder =
                _dbContext
                .Folders
                .SingleOrDefault(x => x.Id == newFolder.Id);

            if (folder == null 
                && newFolder.Foldername.Length > 0
                && newFolder.ParentId != Guid.Empty
                && newFolder.OwnerId != Guid.Empty)
            {
                _dbContext.Folders.Add(newFolder);
                _dbContext.SaveChanges();

                result = true;
            }

            return result;
        }

        public Guid CreateRootFolder(Guid ownerId)
        {
            var rootId = Guid.NewGuid();

            var newRoot = new FolderForm()
                {
                    Id = rootId,
                    ParentId = Guid.Empty,
                    Foldername = "Root",
                    OwnerId = ownerId                
                };

            _dbContext.Folders.Add(newRoot);
            _dbContext.SaveChanges();

            return rootId;
        }

        public IEnumerable<FolderForm> GetChildsFolders(Guid parentId)
        {
            return _dbContext
                .Folders
                .Where(x => x.ParentId == parentId);
        }

        public IEnumerable<FileForm> GetFilesInFolder(Guid folderId)
        {
            return _dbContext
                .Files
                .Where(x => x.FolderId == folderId);
        }

        public bool GetFolderById(Guid folderId, out FolderForm folder)
        {
            bool result = false;

            folder = _dbContext
                .Folders
                .SingleOrDefault(x => x.Id == folderId);
            
            if(folder != null)
            {
                result = true;
            }

            return result;
        }
    }
}