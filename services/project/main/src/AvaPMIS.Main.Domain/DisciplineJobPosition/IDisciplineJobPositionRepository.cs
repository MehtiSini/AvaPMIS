using System.Linq;
using System.Threading.Tasks;
using System;
using Volo.Abp.Domain.Repositories;

namespace AvaPMIS.Main.DisciplineJobPosition
{
    public interface IDisciplineJobPositionRepository : IRepository<DisciplineJobPosition, Guid>
    {
        Task<IQueryable<DisciplineJobPosition>> GetDisciplineJobPositionQuery(IQueryable<DisciplineJobPosition> query);

    }
}