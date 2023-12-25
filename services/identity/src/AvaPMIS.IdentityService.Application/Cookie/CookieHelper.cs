using Microsoft.AspNetCore.Http;
using static AvaPMIS.IdentityService.Cookie.ICookieHelper;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace AvaPMIS.IdentityService.Cookie
{
    public class CookieHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        //Below method makes cookies which will be stay even after user close the app
        public void AddCookie(string key, string value, int expireTime, TimeSpanExpiration time)
        {
            var options = new CookieOptions();

            switch (time)
            {
                case TimeSpanExpiration.day:
                    options.Expires = DateTime.Now.AddDays(expireTime);
                    break;
                case TimeSpanExpiration.minute:
                    options.Expires = DateTime.Now.AddMinutes(expireTime);
                    break;
                case TimeSpanExpiration.second:
                    options.Expires = DateTime.Now.AddSeconds(expireTime);
                    break;
                default:
                    throw new ArgumentException("Invalid expiration unit specified.");
            }

            _httpContextAccessor.HttpContext.Response.Cookies.Append(key, value, options);
        }

        public string GetCookie(string key)
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(key))
            {
                return _httpContextAccessor.HttpContext.Request.Cookies[key];
            }
            return null;
        }

        public void RemoveCookie(string key)
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(key))
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Delete(key);
            }
        }

        public void UpdateCookie(string key, string newValue, int expireTime, TimeSpanExpiration time)
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(key))
            {
                RemoveCookie(key);
            }
            AddCookie(key, newValue, expireTime, time);
        }

        public bool ContainsCookie(string key)
        {
            return _httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(key);
        }

        public void ExpireCookie(string key)
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(key))
            {
                var options = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                _httpContextAccessor.HttpContext.Response.Cookies.Append(key, "", options);
            }
        }


        public int GenerateRndCode()
        {
            Random random = new();

            int randomNumber = random.Next(100000, 1000000);

            return randomNumber;
        }

    }
}
