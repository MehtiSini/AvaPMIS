using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace AvaPMIS.Main.DefDepartment
{
    public interface IDefDepartmentRepository : IRepository<DefDepartment, Guid>
    {
        public Task<IQueryable<DefDepartment>> GetDefDepartmetnsQuery(IQueryable<DefDepartment> query);

    }
}
