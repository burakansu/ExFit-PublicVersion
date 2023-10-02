using Microsoft.AspNetCore.Http;

namespace Core.Helper
{
    public class CookieHelper : ICookieHelper
    {
        private readonly HttpContextAccessor contextAccessor;
        public CookieHelper()
        {
            contextAccessor = new HttpContextAccessor();
        }
        public void Set(string key, string value, int duration)
        {
            value = StringCipher.EncryptString(value);
            var options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(duration);
            contextAccessor?.HttpContext?.Response.Cookies.Append(key, value, options);
        }

        public void Set(string key, string value, DateTime expire)
        {
            value = StringCipher.EncryptString(value);
            var options = new CookieOptions();
            options.Expires = expire;
            contextAccessor?.HttpContext?.Response.Cookies.Append(key, value, options);
        }

        public string Get(string key)
        {
            var value = string.Empty;
            var c = contextAccessor?.HttpContext?.Request.Cookies[key];
            return c != null ? StringCipher.DecryptString(c.ToString()) : value;
        }

        public bool Exists(string key)
        {
            return contextAccessor?.HttpContext?.Request.Cookies[key] != null;
        }

        public void Delete(string key)
        {
            if (Exists(key))
            {
                contextAccessor?.HttpContext?.Response.Cookies.Delete(key);
            }
        }
    }
}