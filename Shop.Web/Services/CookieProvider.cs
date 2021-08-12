using DeamonSharps.Shop.Simple.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace DeamonSharps.Shop.Simple.Services
{
    public class CookieProvider : ICookieProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CookieProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCookieValue(string name)
        {
            return _httpContextAccessor.HttpContext.Request.Cookies?.FirstOrDefault(c => c.Key == name).Value;
        }
    }
}
