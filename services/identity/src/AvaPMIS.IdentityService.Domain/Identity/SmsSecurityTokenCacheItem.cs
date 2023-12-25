using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaPMIS.IdentityService.Identity
{
    public class SmsSecurityTokenCacheItem
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Security Token
        /// </summary>
        public string SecurityToken { get; set; }

        public SmsSecurityTokenCacheItem()
        {

        }

        public SmsSecurityTokenCacheItem(string token, string securityToken)
        {
            Token = token;
            SecurityToken = securityToken;
        }

        /// <summary>
        /// Calculate Cache Key
        /// </summary>
        /// <returns></returns>
        public static string CalculateCacheKey(string phoneNumber, string purpose)
        {
            return "Totp:" + purpose + ";p:" + phoneNumber;
        }
    }
}
