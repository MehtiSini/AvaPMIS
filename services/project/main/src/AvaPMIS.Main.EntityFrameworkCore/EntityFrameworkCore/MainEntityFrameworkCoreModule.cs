using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.EntityFrameworkCore;

using Volo.Abp.Modularity;


namespace AvaPMIS.Main.EntityFrameworkCore;

[DependsOn(
    typeof(MainDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
[DependsOn(typeof(AbpBackgroundJobsModule))]
public class MainEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbContextOptions>(options =>
        {
            //options.UseNpgsql();
            options.UseSqlServer(op=>op.UseNetTopologySuite());
        });
        //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("SqlServer.EnableLegacyTimestampBehavior", true);
        context.Services.AddAbpDbContext<MainDbContext>(options =>
        {
            options.ReplaceDbContext<IMainDbContext>();

            //options.ReplaceDbContext<ISettingManagementDbContext>();
            options.AddDefaultRepositories(true);
            //options.AddRepository<IIdentityUserRepository, EfCoreIdentityUserRepository>();
            //options.AddRepository<Volo.Abp.Identity.IIdentityUserRepository, EfCoreIdentityUserRepository>();

            //options.AddRepository<IIdentityRoleRepository, EfCoreIdentityRoleRepository>();
            //options.AddRepository<Volo.Abp.Identity.IIdentityRoleRepository, EfCoreIdentityRoleRepository>();
        });
    }
}