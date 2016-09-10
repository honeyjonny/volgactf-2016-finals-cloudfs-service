

using CloudFs;
using CloudFs.Controllers;
using CloudFs.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace CloudFS.Controllers
{
    public static class AuthRequestExtensions
    {
        public static IApplicationBuilder UseAuthRequest(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthRequestMiddleware>();
        }

        public static bool GetUser(this HttpRequest request, out UserForm userObj)
        {
            userObj = request.HttpContext.Items[Consts.USER_ITEM] as UserForm;

            return userObj == null ? false : true;
        }

        public static bool GetCookie(this HttpRequest request, out string cookieValue)
        {
            bool result = false;
            cookieValue = null;

            if (request.Cookies.ContainsKey(Consts.COOKIE_KEY))
            {
                cookieValue = request.Cookies[Consts.COOKIE_KEY];
                result = true;
            }

            return result;
        }
    }
}