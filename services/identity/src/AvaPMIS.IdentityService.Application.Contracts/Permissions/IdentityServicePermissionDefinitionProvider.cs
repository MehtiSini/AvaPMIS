using AvaPMIS.IdentityService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace AvaPMIS.IdentityService.Permissions;

public class IdentityServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(IdentityServicePermissions.GroupName, L("Permission:IdentityService"));
    
    myGroup
        .AddPermission(PhoneNumberLoginPermissions.UserLookup.Default, L("Permission:UserLookup"))
    .WithProviders(ClientPermissionValueProvider.ProviderName);
}


private static LocalizableString L(string name)
    {
        return LocalizableString.Create<IdentityServiceResource>(name);
    }
}