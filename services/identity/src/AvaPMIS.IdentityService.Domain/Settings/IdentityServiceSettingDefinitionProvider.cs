using AvaPMIS.IdentityService.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace AvaPMIS.IdentityService.Settings;

public class IdentityServiceSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(new SettingDefinition(
                IdentityServiceSettingNames.RegisterCodeCacheSeconds,
                "300",
                L("RegisterCodeCacheSeconds")),

            new SettingDefinition(
                    name: IdentityServiceSettingNames.SmsRepetInterval,
                    defaultValue: "2",
                    displayName: L("DisplayName:IdentityService.PhoneNumberLogin.SmsRepetInterval"),
                    description: L("Description:IdentityService.PhoneNumberLogin.SmsRepetInterval"),
                    isVisibleToClients: true)
                .WithProviders(
                    DefaultValueSettingValueProvider.ProviderName,
                    GlobalSettingValueProvider.ProviderName,
                    TenantSettingValueProvider.ProviderName),
            new SettingDefinition(
                    name: IdentityServiceSettingNames.SmsUserSignin,
                    defaultValue: "your sign in code: {0}",
                    displayName: L("DisplayName:IdentityService.PhoneNumberLogin.SmsUserSignin"),
                    description: L("Description:IdentityService.PhoneNumberLogin.SmsUserSignin"),
                    isVisibleToClients: true)
                .WithProviders(
                    DefaultValueSettingValueProvider.ProviderName,
                    GlobalSettingValueProvider.ProviderName,
                    TenantSettingValueProvider.ProviderName));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<IdentityServiceResource>(name);
    }
}