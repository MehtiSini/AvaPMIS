using AvaPMIS.IdentityService.Utils;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Domain;
using Volo.Abp.Identity;
using Volo.Abp.OpenIddict;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.PermissionManagement.OpenIddict;
using AvaPMIS.IdentityService.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using AvaPMIS.IdentityService.OpenIddict;
using Volo.Abp.OpenIddict.ExtensionGrantTypes;

namespace AvaPMIS.IdentityService;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(IdentityServiceDomainSharedModule)
)]
[DependsOn(typeof(AbpIdentityDomainModule))]
[DependsOn(typeof(AbpPermissionManagementDomainIdentityModule))]
[DependsOn(typeof(AbpOpenIddictDomainModule))]
[DependsOn(typeof(AbpPermissionManagementDomainOpenIddictModule))]
public class IdentityServiceDomainModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<OpenIddictServerBuilder>(builder =>
        {
            builder.Configure(openIddictServerOptions =>
            {
                openIddictServerOptions.GrantTypes.Add(PhoneNumberLoginConsts.GrantType);
            });
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.TryAddTransient<IPhoneNumberLoginOrNewUserCreator, DefaultPhoneLoginOrNewUserCreator>();
        context.Services.TryAddTransient<UniquePhoneNumberUserValidator>();
        context.Services.AddTransient<IUserValidator<Volo.Abp.Identity.IdentityUser>, UniquePhoneNumberUserValidator>();

        context.Services.Configure<AbpOpenIddictExtensionGrantsOptions>(options =>
        {
            options.Grants.Add(PhoneNumberLoginConsts.GrantType,
                new PhoneNumberLoginTokenExtensionGrant());
        });
        context.Services.Configure<AbpOpenIddictClaimsPrincipalOptions>(options =>
        {
            options.ClaimsPrincipalHandlers.Add<IdentityServiceClaimDestinationsHandler>();
        });

    }
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        context
            .ServiceProvider
            .GetRequiredService<SnowflakeIdGenerator>()
            .Initialize();


    }
}