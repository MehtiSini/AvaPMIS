using Microsoft.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace AvaPMIS.Main.EntityFrameworkCore;

[ConnectionStringName(MainDbProperties.ConnectionStringName)]
public interface IMainDbContext : IEfCoreDbContext
{
   
    DbSet<BackgroundJobRecord> BackgroundJobs { get; set; }
}