namespace Core.Helper
{
    public interface ICookieHelper
    {
        public void Set(string key, string value, int duration);
        public void Set(string key, string value, DateTime expire);
        public string Get(string key);
        public bool Exists(string key);
        public void Delete(string key);
    }
}