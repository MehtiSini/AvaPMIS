using AvaPMIS.Main.Company;
using AvaPMIS.Main.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace AvaPMIS.Main.CompanyDepartment
{
    public class EfCompanyDepartmentRepository : EfCoreRepository<MainDbContext, CompanyDepartment, Guid>, ICompanyDepartmentRepository
    {

        public EfCompanyDepartmentRepository(IDbContextProvider<MainDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public async Task<IQueryable<CompanyDepartment>> GetCompanyDepartmentQuery(IQueryable<CompanyDepartment> query)
        {
            return query;
        }
    }
}
