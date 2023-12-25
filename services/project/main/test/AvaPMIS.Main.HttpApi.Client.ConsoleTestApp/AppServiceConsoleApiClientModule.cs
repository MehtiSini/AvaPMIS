using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace AvaPMIS.Main;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(MainHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class MainConsoleApiClientModule : AbpModule
{

}
