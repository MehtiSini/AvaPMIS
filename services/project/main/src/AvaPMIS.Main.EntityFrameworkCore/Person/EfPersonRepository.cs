using AvaPMIS.Main.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace AvaPMIS.Main.Person
{
    public class EfPersonRepository : EfCoreRepository<MainDbContext, Main.Person.Person, Guid>, IPersonRepository
    {

        public EfPersonRepository(IDbContextProvider<MainDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public async Task<IQueryable<Person>> GetDisciplineQuery(IQueryable<Person> query)
        {
            return query;
        }
    }
}