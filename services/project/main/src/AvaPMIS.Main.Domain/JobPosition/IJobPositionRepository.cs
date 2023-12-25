using System.Linq;
using System.Threading.Tasks;
using System;
using Volo.Abp.Domain.Repositories;

namespace AvaPMIS.Main.JobPosition
{
    public interface IJobPositionRepository : IRepository<JobPosition, Guid>
    {
        Task<IQueryable<JobPosition>> GetDisciplineQuery(IQueryable<JobPosition> query);

    }
}