using System;
using System.Linq;
using System.Threading.Tasks;
using AvaPMIS.Main.Department;
using AvaPMIS.Main.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace AvaPMIS.Main.Discipline
{
    public class EfDisciplineRepository : EfCoreRepository<MainDbContext, Main.Discipline.Discipline, Guid>, IDisciplineRepository
    {

        public EfDisciplineRepository(IDbContextProvider<MainDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public async Task<IQueryable<Discipline>> GetDisciplineQuery(IQueryable<Discipline> query)
        {
            return query;
        }
    }
}
