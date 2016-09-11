
using CloudFs.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace CloudFs.Services
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserForm> Users {get;set;}

        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasPostgresExtension("uuid-ossp");

            var userBuilder = builder.Entity<UserForm>();
            userBuilder.HasKey(x => x.Id);
            userBuilder.ToTable("app_users");
        }
        
    }
}