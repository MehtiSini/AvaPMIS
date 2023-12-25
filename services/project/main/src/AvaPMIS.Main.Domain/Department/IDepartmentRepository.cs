using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace AvaPMIS.Main.Department
{
    public interface IDepartmentRepository : IRepository<Department, Guid>
    {
        Task<IQueryable<Department>> GetDepartmentQuery(IQueryable<Department> query);

    }
}
