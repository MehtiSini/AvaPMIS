using System;
using System.Diagnostics;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AvaPMIS.Main.EntityFrameworkCore;

public class MainDbContextFactory : IDesignTimeDbContextFactory<MainDbContext>
{
    public MainDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        //var builder = new DbContextOptionsBuilder<MainDbContext>()
        //    .UseNpgsql(GetConnectionStringFromConfiguration());
        var builder = new DbContextOptionsBuilder<MainDbContext>()
            .UseSqlServer(GetConnectionStringFromConfiguration(),x=>x.UseNetTopologySuite());
        return new MainDbContext(builder.Options);
    }

    private static string GetConnectionStringFromConfiguration()
    {
        return BuildConfiguration()
            .GetConnectionString(MainDbProperties.ConnectionStringName);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var basePath= Path.Combine(
            Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName,
            $"host{Path.DirectorySeparatorChar}AvaPMIS.Main.HttpApi.Host"
        ).Replace("administration\\host", "main\\host");
        var builder = new ConfigurationBuilder()
            .SetBasePath(
                basePath
            )
            .AddJsonFile("appsettings.json", false);

        return builder.Build();
    }
}