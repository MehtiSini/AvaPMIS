using Volo.Abp.Modularity;

namespace AvaPMIS.Administration;

[DependsOn(
    typeof(AdministrationApplicationModule),
    typeof(AdministrationDomainTestModule)
    )]
public class AdministrationApplicationTestModule : AbpModule
{

}
