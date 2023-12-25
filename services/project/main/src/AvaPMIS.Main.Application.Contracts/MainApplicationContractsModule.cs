using Volo.Abp.Account;
using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace AvaPMIS.Main;

[DependsOn(
    typeof(MainDomainSharedModule),
    typeof(AbpDddApplicationContractsModule)
)]
public class MainApplicationContractsModule : AbpModule
{
}