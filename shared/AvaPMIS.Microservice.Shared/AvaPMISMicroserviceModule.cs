using AvaPMIS.Administration.EntityFrameworkCore;
using AvaPMIS.Hosting.Shared;
using Volo.Abp.Modularity;


namespace AvaPMIS.Microservice.Shared;

[DependsOn(
    typeof(AvaPMISHostingModule),
    typeof(AdministrationEntityFrameworkCoreModule)
)]

    public class AvaPMISMicroserviceModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }
}