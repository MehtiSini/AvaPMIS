using System.Linq;
using System.Threading.Tasks;
using System;
using Volo.Abp.Domain.Repositories;

namespace AvaPMIS.Main.Discipline
{
    public interface IDisciplineRepository : IRepository<Discipline, Guid>
    {
        Task<IQueryable<Discipline>> GetDisciplineQuery(IQueryable<Discipline> query);

    }
}
