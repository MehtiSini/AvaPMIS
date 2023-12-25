using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace AvaPMIS.IdentityService.Identity
{
    //Todo complete it later
    public class CustomTotpTokenProvider<TUser> : DataProtectorTokenProvider<TUser>
        where TUser : IdentityUser
    {
        public CustomTotpTokenProvider(
            IDataProtectionProvider dataProtectionProvider,
            IOptions<TotpTokenProviderOptions> options, ILogger<CustomTotpTokenProvider<TUser>> logger)
            : base(dataProtectionProvider, options, logger)
        { }
    }

    public static class TotpTokenProviderIdentityBuilderExtensions
    {
        public static IdentityBuilder AddNPTokenProvider(this IdentityBuilder builder)
        {
            var userType = builder.UserType;
            var provider = typeof(CustomTotpTokenProvider<>).MakeGenericType(userType);
            return builder.AddTokenProvider("TotpTokenProvider", provider);
        }
    }
    public class TotpTokenProviderOptions : DataProtectionTokenProviderOptions
    {
        public TotpTokenProviderOptions()
        {
            Name = "CustomTotpTokenProvider";
            TokenLifespan = TimeSpan.FromMinutes(10);
        }
    }
}