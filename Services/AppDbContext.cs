
using CloudFs.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace CloudFs.Services
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserForm> Users {get;set;}
        public DbSet<FolderForm> Folders {get;set;}
        public DbSet<FileForm> Files {get;set;}

        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasPostgresExtension("uuid-ossp");

            var userBuilder = builder.Entity<UserForm>();
            userBuilder.HasKey(x => x.Id);
            userBuilder.ToTable("app_users");

            var foldersBuilder = builder.Entity<FolderForm>();
            foldersBuilder.HasKey(x => x.Id);
            foldersBuilder.HasIndex(x => x.ParentId);
            foldersBuilder.ToTable("app_folders");

            var filesBuilder = builder.Entity<FileForm>();
            filesBuilder.HasKey(x => x.Id);
            filesBuilder.HasIndex(x => x.FolderId);
            filesBuilder.ToTable("app_files");
        }        
    }
}