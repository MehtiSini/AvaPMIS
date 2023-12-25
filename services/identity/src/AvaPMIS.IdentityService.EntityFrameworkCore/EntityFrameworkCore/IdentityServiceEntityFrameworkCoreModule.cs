using System;
using AvaPMIS.IdentityService.Identity;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.Modularity;
using EfCoreIdentityRoleRepository = AvaPMIS.IdentityService.Identity.EfCoreIdentityRoleRepository;
using EfCoreIdentityUserRepository = AvaPMIS.IdentityService.Identity.EfCoreIdentityUserRepository;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace AvaPMIS.IdentityService.EntityFrameworkCore;

[DependsOn(
    typeof(IdentityServiceDomainModule),
    typeof(AbpEntityFrameworkCoreModule),

    typeof(AbpIdentityEntityFrameworkCoreModule),
    typeof(AbpOpenIddictEntityFrameworkCoreModule)
)]
public class IdentityServiceEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbContextOptions>(options =>
        {
            //options.UseNpgsql();
            options.UseSqlServer();
        });
        //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("SqlServer.EnableLegacyTimestampBehavior", true);
        context.Services.AddAbpDbContext<IdentityServiceDbContext>(options =>
        {
            options.ReplaceDbContext<IIdentityDbContext>();
            options.ReplaceDbContext<IOpenIddictDbContext>();
            //options.ReplaceDbContext<ISettingManagementDbContext>();
            options.AddDefaultRepositories(true);
            //options.AddRepository<IIdentityUserRepository, EfCoreIdentityUserRepository>();
            //options.AddRepository<Volo.Abp.Identity.IIdentityUserRepository, EfCoreIdentityUserRepository>();

            //options.AddRepository<IIdentityRoleRepository, EfCoreIdentityRoleRepository>();
            //options.AddRepository<Volo.Abp.Identity.IIdentityRoleRepository, EfCoreIdentityRoleRepository>();
        });
    }
}