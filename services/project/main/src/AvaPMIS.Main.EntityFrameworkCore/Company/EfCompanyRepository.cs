using AvaPMIS.Main.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace AvaPMIS.Main.Company
{
    public class EfCompanyRepository : EfCoreRepository<MainDbContext, Main.Company.Company, Guid>, ICompanyRepository
    {

        public EfCompanyRepository(IDbContextProvider<MainDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public async Task<IQueryable<Company>> GetCompanyQuery(IQueryable<Company> query)
        {
            return query;
        }
    }
}