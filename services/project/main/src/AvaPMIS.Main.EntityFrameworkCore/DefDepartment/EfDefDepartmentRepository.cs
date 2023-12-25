using AvaPMIS.Main.Company;
using AvaPMIS.Main.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace AvaPMIS.Main.DefDepartment
{
    public class EfDefDepartmentRepository : EfCoreRepository<MainDbContext, DefDepartment, Guid>, IDefDepartmentRepository
    {

        public EfDefDepartmentRepository(IDbContextProvider<MainDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public async Task<IQueryable<DefDepartment>> GetDefDepartmetnsQuery(IQueryable<DefDepartment> query)
        {
            return query;
        }
    }
}