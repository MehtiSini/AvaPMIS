using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Validation;

namespace AvaPMIS.Main.DisciplineJobPosition
{
    public class DisciplineJobPositionAppService : CrudAppService<DisciplineJobPosition, DisciplineJobPositionDto, Guid, PagedAndSortedResultRequestDto,
        CreateUpdateDisciplineJobPositionDto, CreateUpdateDisciplineJobPositionDto>, IDisciplineJobPositionAppService, ITransientDependency, IValidationEnabled

    {
        public DisciplineJobPositionAppService(IRepository<DisciplineJobPosition, Guid> repository) : base(repository)
        {
        }

       
    }
}