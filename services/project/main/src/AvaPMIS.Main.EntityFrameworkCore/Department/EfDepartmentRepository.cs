using AvaPMIS.Main.Company;
using AvaPMIS.Main.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace AvaPMIS.Main.Department
{
    public class EfDepartmentRepository : EfCoreRepository<MainDbContext, Main.Department.Department, Guid>, IDepartmentRepository
    {

        public EfDepartmentRepository(IDbContextProvider<MainDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public async Task<IQueryable<Department>> GetDepartmentQuery(IQueryable<Department> query)
        {
            return query;
        }
    }
}
