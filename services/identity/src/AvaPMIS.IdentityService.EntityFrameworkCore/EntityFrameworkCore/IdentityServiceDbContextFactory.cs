using System;
using System.Diagnostics;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AvaPMIS.IdentityService.EntityFrameworkCore;

public class IdentityServiceDbContextFactory : IDesignTimeDbContextFactory<IdentityServiceDbContext>
{
    public IdentityServiceDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        //var builder = new DbContextOptionsBuilder<IdentityServiceDbContext>()
        //    .UseNpgsql(GetConnectionStringFromConfiguration());
        var builder = new DbContextOptionsBuilder<IdentityServiceDbContext>()
            .UseSqlServer(GetConnectionStringFromConfiguration());
        return new IdentityServiceDbContext(builder.Options);
    }

    private static string GetConnectionStringFromConfiguration()
    {
        return BuildConfiguration()
            .GetConnectionString(IdentityServiceDbProperties.ConnectionStringName);
    }

    private static IConfigurationRoot BuildConfiguration()
    {

        var builder = new ConfigurationBuilder()
            .SetBasePath(
                Path.Combine(
                    Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName,
                    $"host{Path.DirectorySeparatorChar}AvaPMIS.IdentityService.HttpApi.Host"
                ).Replace("administration\\host", "identity\\host")
            )
            .AddJsonFile("appsettings.json", false);

        return builder.Build();
    }
}