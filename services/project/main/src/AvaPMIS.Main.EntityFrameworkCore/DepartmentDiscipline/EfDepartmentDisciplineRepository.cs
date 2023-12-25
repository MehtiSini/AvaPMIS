using System;
using System.Linq;
using System.Threading.Tasks;
using AvaPMIS.Main.DepartmentDiscipline;
using AvaPMIS.Main.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace AvaPMIS.Main.Discipline
{
    public class EfDepartmentDisciplineRepository : EfCoreRepository<MainDbContext, DepartmentDiscipline.DepartmentDiscipline, Guid>, IDepartmentDisciplineRepository
    {

        public EfDepartmentDisciplineRepository(IDbContextProvider<MainDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public async Task<IQueryable<DepartmentDiscipline.DepartmentDiscipline>> GetDepartmentDisciplineQuery(IQueryable<DepartmentDiscipline.DepartmentDiscipline> query)
        {
            return query;
        }
    }
}
