using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace AvaPMIS.Main.Person
{
    public interface IPersonRepository : IRepository<Person, Guid>
    {
        Task<IQueryable<Person>> GetDisciplineQuery(IQueryable<Person> query);

    }
}