using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.OpenIddict;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace AvaPMIS.IdentityService.OpenIddict
{
    /// <summary>
    /// Custom IAbpOpenIddictClaimsPrincipalHandler to include roles in access token
    /// </summary>
    public class IdentityServiceClaimDestinationsHandler : IAbpOpenIddictClaimsPrincipalHandler, ITransientDependency
    {
        private IdentityUserManager _userManager;

        public IdentityServiceClaimDestinationsHandler(IdentityUserManager userManager)
        {
            _userManager = userManager;
        }
        public async virtual Task HandleAsync(AbpOpenIddictClaimsPrincipalHandlerContext context)
        {
            foreach (var scope in context.OpenIddictRequest.GetScopes())
            {
                context.Principal.AddClaim(Claims.Scope, scope);
            }


            if (context.Principal.Identity != null)
            {

            }
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            //claimsIdentity.RoleClaimType = OpenIddict.Abstractions.OpenIddictConstants.Claims.Role;
            //claimsIdentity.NameClaimType = OpenIddict.Abstractions.OpenIddictConstants.Claims.Name;

            //ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            ////claimsIdentity.RoleClaimType = OpenIddict.Abstractions.OpenIddictConstants.Claims.Role;
            ////claimsIdentity.NameClaimType = OpenIddict.Abstractions.OpenIddictConstants.Claims.Name;


            //var claimType = ClaimTypes.Role;


            //if (context.Principal.Identity != null && context.Principal.Identity.IsAuthenticated && !string.IsNullOrEmpty(context.Principal.Identity?.Name))
            //{
            //    //Do I already have roles in the claim?
            //    var roleClaimsAvailable = context.Principal.Claims.Any(x => x.Type == claimType);
            //    if (!roleClaimsAvailable)
            //    {
            //        //Roles not found, adding:
            //        var userProfile = await _userManager.FindByNameAsync(context.Principal.Identity.Name);
            //        if (userProfile != null)
            //        {
            //            var roles = await _userManager.GetRolesAsync(userProfile);
            //            foreach (var role in roles)
            //            {
            //                claimsIdentity.AddClaim(new Claim(claimType, role));
            //                claimsIdentity.AddClaim(new Claim(OpenIddictConstants.Claims.Role, role));
            //            }

            //            context.Principal.AddIdentity(claimsIdentity);
            //        }
            //    }
            //}

        }
    }
}