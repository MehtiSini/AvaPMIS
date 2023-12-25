using Volo.Abp.Modularity;

namespace AvaPMIS.Main;

[DependsOn(
    typeof(MainApplicationModule),
    typeof(MainDomainTestModule)
    )]
public class MainApplicationTestModule : AbpModule
{

}
