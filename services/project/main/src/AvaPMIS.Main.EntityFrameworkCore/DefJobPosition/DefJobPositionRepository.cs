using AvaPMIS.Main.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace AvaPMIS.Main.DefJobPosition
{
    public class DefJobPositionRepository : EfCoreRepository<MainDbContext, DefJobPosition, Guid>, IDefJobPositionRepository
    {

        public DefJobPositionRepository(IDbContextProvider<MainDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public async Task<IQueryable<DefJobPosition>> GetDefJobPositionsQuery(IQueryable<DefJobPosition> query)
        {
            return query;
        }
    }
}