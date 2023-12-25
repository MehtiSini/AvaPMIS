using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AvaPMIS.SaaS.EntityFrameworkCore;

public class SaaSDbContextFactory : IDesignTimeDbContextFactory<SaaSDbContext>
{
    public SaaSDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        //var builder = new DbContextOptionsBuilder<SaaSDbContext>()
        //    .UseNpgsql(GetConnectionStringFromConfiguration());
        var builder = new DbContextOptionsBuilder<SaaSDbContext>()
            .UseSqlServer(GetConnectionStringFromConfiguration());
        return new SaaSDbContext(builder.Options);
    }

    private static string GetConnectionStringFromConfiguration()
    {
        return BuildConfiguration()
            .GetConnectionString(SaaSDbProperties.ConnectionStringName);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(
                Path.Combine(
                    Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName,
                    $"host{Path.DirectorySeparatorChar}AvaPMIS.SaaS.HttpApi.Host"
                ).Replace("administration\\host", "saas\\host")
            )
            .AddJsonFile("appsettings.json", false);

        return builder.Build();
    }
}