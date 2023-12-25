using AvaPMIS.Main.DefDepartment;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Validation;

namespace AvaPMIS.Main.DefDiscipline
{
    public class DefDisciplineAppService : CrudAppService<DefDiscipline, DefDisciplineDto, Guid, PagedAndSortedResultRequestDto,
        CreateUpdateDefDisciplineDto, CreateUpdateDefDisciplineDto>, IDefDisciplineAppService, ITransientDependency, IValidationEnabled

    {
        public DefDisciplineAppService(IRepository<DefDiscipline, Guid> repository) : base(repository)
        {
        }
    }
}
