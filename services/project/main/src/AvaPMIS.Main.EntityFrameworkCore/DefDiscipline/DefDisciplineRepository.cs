using AvaPMIS.Main.DefDepartment;
using AvaPMIS.Main.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace AvaPMIS.Main.DefDiscipline
{
    public class DefDisciplineRepository : EfCoreRepository<MainDbContext, DefDiscipline, Guid>, IDefDisciplineRepository
    {

        public DefDisciplineRepository(IDbContextProvider<MainDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public async Task<IQueryable<DefDiscipline>> GetDefDisciplinesQuery(IQueryable<DefDiscipline> query)
        {
            return query;
        }
    }
}
