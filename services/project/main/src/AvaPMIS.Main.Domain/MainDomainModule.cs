using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;


namespace AvaPMIS.Main;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(MainDomainSharedModule)
)]

public class MainDomainModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {

    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        
    }
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {

    }
}