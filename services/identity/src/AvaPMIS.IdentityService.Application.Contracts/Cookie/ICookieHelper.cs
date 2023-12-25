namespace AvaPMIS.IdentityService.Cookie
{
    public interface ICookieHelper
    {
        void AddCookie(string key, string value, int expireTime, TimeSpanExpiration time);
        string GetCookie(string key);
        void RemoveCookie(string key);
        void UpdateCookie(string key, string newValue, int expireTime, TimeSpanExpiration time);
        bool ContainsCookie(string key);
        void ExpireCookie(string key);
        int GenerateRndCode();


        public enum TimeSpanExpiration
        {
            day = 1,
            minute,
            second
        }

    }
}
