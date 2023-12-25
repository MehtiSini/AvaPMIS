using AvaPMIS.Administration;
using AvaPMIS.Administration.EntityFrameworkCore;
using AvaPMIS.IdentityService;
using AvaPMIS.IdentityService.EntityFrameworkCore;
using AvaPMIS.Main;
using AvaPMIS.Main.EntityFrameworkCore;
using AvaPMIS.SaaS;
using AvaPMIS.SaaS.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace AvaPMIS.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AdministrationEntityFrameworkCoreModule),
    typeof(AdministrationApplicationContractsModule),
    typeof(IdentityServiceEntityFrameworkCoreModule),
    typeof(IdentityServiceApplicationContractsModule),
    typeof(SaaSEntityFrameworkCoreModule),
    typeof(SaaSApplicationContractsModule),
typeof(MainEntityFrameworkCoreModule),
typeof(MainApplicationContractsModule)
)]
public class AvaPMISDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {

        //context.Services.AddTransient<PersonDataSeeder>();
        //context.Services.AddTransient<MainDataSeederContributor>();

        ////Todo consider
        ////Added bcz automatic migration faced with error due to the use of NetTopologySuite Point
        //Configure<AbpDbContextOptions>(options =>
        //{
        //    //options.UseNpgsql();
        //    options.UseSqlServer(x => x.UseNetTopologySuite());
        //});

        //Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}