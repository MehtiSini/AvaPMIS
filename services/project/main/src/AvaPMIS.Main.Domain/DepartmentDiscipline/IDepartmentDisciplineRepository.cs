using System.Linq;
using System.Threading.Tasks;
using System;
using Volo.Abp.Domain.Repositories;

namespace AvaPMIS.Main.DepartmentDiscipline
{
    public interface IDepartmentDisciplineRepository : IRepository<DepartmentDiscipline, Guid>
    {
        Task<IQueryable<DepartmentDiscipline>> GetDepartmentDisciplineQuery(IQueryable<DepartmentDiscipline> query);

    }
}
