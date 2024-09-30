using Microsoft.AspNetCore.Http;

namespace Learning.Mvc.PresentationExtensions
{
    public static class HttpExtensions
    {
        public static string GetUserIp(this HttpContext httpContext)
        {
            return httpContext.Connection.RemoteIpAddress.ToString();
        }

        public static bool IsUserAuthenticated(this HttpContext httpContext)
        {
            return httpContext.User.Identity.IsAuthenticated;
        }

        public static string GetCurrentUserName(this HttpContext httpContext)
        {
            return httpContext.User.Identity.Name;
        }
    }
}
