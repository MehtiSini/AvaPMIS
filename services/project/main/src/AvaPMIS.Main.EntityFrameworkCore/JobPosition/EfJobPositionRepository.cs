using AvaPMIS.Main.Discipline;
using AvaPMIS.Main.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace AvaPMIS.Main.JobPosition
{
    public class EfJobPositionRepository : EfCoreRepository<MainDbContext, Main.JobPosition.JobPosition, Guid>, IJobPositionRepository
    {

        public EfJobPositionRepository(IDbContextProvider<MainDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public async Task<IQueryable<JobPosition>> GetDisciplineQuery(IQueryable<JobPosition> query)
        {
            return query;
        }
    }
}
