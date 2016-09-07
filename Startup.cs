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
            services.AddMvc();

            services.AddSingleton<IUsersRepository,UsersRepository>();
        }

        public void Configure(IApplicationBuilder app)
        {  

            app.UseAuthRequest();

            app.UseMvc();    
        }
    }
}