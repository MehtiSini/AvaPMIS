using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Volo.Abp;
using Volo.Abp.Identity;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace AvaPMIS.IdentityService.Identity
{
    public static class IdentityUserManagerExtensions
    {
        public static async Task<IdentityResult> SetTwoFactorEnabledWithAccountConfirmedAsync<TUser>(
            [NotNull] this UserManager<TUser> userManager, 
            [NotNull] TUser user, 
            bool enabled)
            where TUser : class
        {
            Check.NotNull(userManager, nameof(userManager));
            Check.NotNull(user, nameof(user));
            
            if (enabled)
            {
                var phoneNumberConfirmed = await userManager.IsPhoneNumberConfirmedAsync(user);
                var emailAddressConfirmed = await userManager.IsEmailConfirmedAsync(user);

                if (!phoneNumberConfirmed && !emailAddressConfirmed)
                {
                    // TODO: 返回标准的 IdentityResult
                    //var error = new IdentityError();
                    //return IdentityResult.Failed(error);

                    throw new IdentityException(
                        Volo.Abp.Identity.IdentityErrorCodes.CanNotChangeTwoFactor,
                        details: phoneNumberConfirmed ? "phone number not confirmed" : "email address not confirmed");
                }
            }

            return await userManager.SetTwoFactorEnabledAsync(user, enabled);
        }

     
    }

    
}
