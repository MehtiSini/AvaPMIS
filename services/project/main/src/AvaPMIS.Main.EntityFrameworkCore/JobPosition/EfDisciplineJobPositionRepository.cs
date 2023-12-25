using AvaPMIS.Main.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using AvaPMIS.Main.DisciplineJobPosition;

namespace AvaPMIS.Main.JobPosition
{
    public class EfDisciplineJobPositionRepository : EfCoreRepository<MainDbContext, Main.DisciplineJobPosition.DisciplineJobPosition, Guid>, IDisciplineJobPositionRepository
    {

        public EfDisciplineJobPositionRepository(IDbContextProvider<MainDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public async Task<IQueryable<DisciplineJobPosition.DisciplineJobPosition>> GetDisciplineJobPositionQuery(IQueryable<DisciplineJobPosition.DisciplineJobPosition> query)
        { 
            return query;
        }
    }
}
