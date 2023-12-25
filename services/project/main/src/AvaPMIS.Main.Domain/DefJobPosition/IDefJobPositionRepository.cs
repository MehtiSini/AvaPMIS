using System.Linq;
using System.Threading.Tasks;
using System;
using Volo.Abp.Domain.Repositories;

namespace AvaPMIS.Main.DefJobPosition
{
    public interface IDefJobPositionRepository : IRepository<DefJobPosition, Guid>
    {
        public Task<IQueryable<DefJobPosition>> GetDefJobPositionsQuery(IQueryable<DefJobPosition> query);

    }
}
