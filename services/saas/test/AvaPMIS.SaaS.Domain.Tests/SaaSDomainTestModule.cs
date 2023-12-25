using AvaPMIS.SaaS.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace AvaPMIS.SaaS;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(SaaSEntityFrameworkCoreTestModule)
    )]
public class SaaSDomainTestModule : AbpModule
{

}
