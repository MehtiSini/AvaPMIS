using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Validation;

namespace AvaPMIS.Main.Person
{
    public class PersonAppService : CrudAppService<Person, PersonDto, Guid, PagedAndSortedResultRequestDto,
        CreateUpdatePersonDto, CreateUpdatePersonDto>, IPersonAppService, ITransientDependency, IValidationEnabled

    {
        public PersonAppService(IRepository<Person, Guid> repository) : base(repository)
        {
        }


    }
}