

using CloudFs.Controllers;
using Microsoft.AspNetCore.Builder;

namespace CloudFS.Controllers
{
    public static class AuthRequestExtensions
    {
        public static IApplicationBuilder UseAuthRequest(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthRequestMiddleware>();
        }
    }
}