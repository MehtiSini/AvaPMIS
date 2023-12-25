using AvaPMIS.Main.DepartmentDiscipline;
using AvaPMIS.Main.Discipline;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Validation;

namespace AvaPMIS.Main.DefJobPosition
{
    public class DefJobPositionAppService : CrudAppService<DefJobPosition, DefJobPositionDto, Guid, PagedAndSortedResultRequestDto,
        CreateUpdateJobPositionDto, CreateUpdateJobPositionDto>, IDefJobPositionAppService, ITransientDependency, IValidationEnabled

    {
        public DefJobPositionAppService(IRepository<DefJobPosition, Guid> repository) : base(repository)
        {
        }


    }
}