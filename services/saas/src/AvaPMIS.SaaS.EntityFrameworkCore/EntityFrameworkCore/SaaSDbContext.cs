using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace AvaPMIS.SaaS.EntityFrameworkCore;

[ConnectionStringName(SaaSDbProperties.ConnectionStringName)]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ReplaceDbContext(typeof(ISaaSDbContext))]
public class SaaSDbContext : AbpDbContext<SaaSDbContext>, ITenantManagementDbContext, ISaaSDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public SaaSDbContext(DbContextOptions<SaaSDbContext> options)
        : base(options)
    {
    }

    public DbSet<Tenant> Tenants { get; set; }

    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureSaaS();
        builder.ConfigureTenantManagement();
    }
}