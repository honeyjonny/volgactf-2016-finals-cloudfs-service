using CloudFs.Services;
using CloudFS.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CloudFs
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();

            services.AddMvc();

            services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseNpgsql(Consts.CONNECTION_STRING);                    
                });

            services.AddScoped<IUsersRepository,UsersRepository>();
            services.AddScoped<IFoldersRepository, FoldersRepository>();
            services.AddScoped<IFilesRepository, FilesRepository>();

            services.AddSingleton<ISessionRepository, SessionRepository>();

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider
                .GetService<AppDbContext>()
                .Database
                .EnsureCreated();                
        }

        public void Configure(IApplicationBuilder app)
        {  
            app.UseAuthRequest();

            app.UseMvc();    
        }
    }
}