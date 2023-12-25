using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace AvaPMIS.Main.Company
{
    public interface ICompanyRepository : IRepository<Company, Guid>
    {
        Task<IQueryable<Company>> GetCompanyQuery(IQueryable<Company> query);

    }
}
