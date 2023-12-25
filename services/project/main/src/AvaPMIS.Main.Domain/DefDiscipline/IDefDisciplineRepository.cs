using System.Linq;
using System.Threading.Tasks;
using System;
using Volo.Abp.Domain.Repositories;

namespace AvaPMIS.Main.DefDiscipline
{
    public interface IDefDisciplineRepository : IRepository<DefDiscipline, Guid>
    {
        public Task<IQueryable<DefDiscipline>> GetDefDisciplinesQuery(IQueryable<DefDiscipline> query);

    }
}
