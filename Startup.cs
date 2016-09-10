using CloudFs.Services;
using CloudFS.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CloudFs
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();

            services.AddMvc();

            services.AddSingleton<IUsersRepository,UsersRepository>();

            services.AddSingleton<ISessionRepository, SessionRepository>();
        }

        public void Configure(IApplicationBuilder app)
        {  
            app.UseAuthRequest();

            app.UseMvc();    
        }
    }
}