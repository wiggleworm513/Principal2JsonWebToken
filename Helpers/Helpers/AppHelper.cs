using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Principal;

namespace Helpers
{
    public static class AppHelper
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            Config = configuration;
        }

        public static HttpContext Current => _httpContextAccessor.HttpContext;
        public static IPrincipal User => _httpContextAccessor.HttpContext.User;
        public static IConfiguration Config { get; private set; }

        public static string GetUserName()
        {
            return User.Identity.Name.Split('\\', StringSplitOptions.RemoveEmptyEntries)[1];
        }

    }
}
