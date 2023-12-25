using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace AvaPMIS.Main.CompanyDepartment
{
    public interface ICompanyDepartmentRepository : IRepository<CompanyDepartment, Guid>
    {
        Task<IQueryable<CompanyDepartment>> GetCompanyDepartmentQuery(IQueryable<CompanyDepartment> query);

    }
}
