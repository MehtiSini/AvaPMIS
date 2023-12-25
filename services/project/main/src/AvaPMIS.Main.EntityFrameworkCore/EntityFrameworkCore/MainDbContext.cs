using Microsoft.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace AvaPMIS.Main.EntityFrameworkCore;

[ConnectionStringName(MainDbProperties.ConnectionStringName)]
[ReplaceDbContext(typeof(IMainDbContext))]

public class MainDbContext : AbpDbContext<MainDbContext>, IMainDbContext, IBackgroundJobsDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */


    public DbSet<BackgroundJobRecord> BackgroundJobs { get; set; }


    public DbSet<Company.Company> Companies{ get; set; }
    public DbSet<Department.Department> Departments{ get; set; }
    public DbSet<Discipline.Discipline> Disciplines{ get; set; }
    public DbSet<JobPosition.JobPosition> JobPositions{ get; set; }
    public DbSet<Person.Person> People{ get; set; }


    public MainDbContext(DbContextOptions<MainDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureBackgroundJobs();
        builder.ConfigureMain();
        builder.ApplyConfigurationsFromAssembly(typeof(MainDbContext).Assembly);
    }


}