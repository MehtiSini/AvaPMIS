using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Validation;
using AvaPMIS.Main.Discipline;
using AvaPMIS.Main.DepartmentDiscipline;

namespace AvaPMIS.Main.DepartmentDescipline
{
    public class DepartmentDisciplineAppService : CrudAppService<DepartmentDiscipline.DepartmentDiscipline, DepartmentDisciplineDto, Guid, PagedAndSortedResultRequestDto,
        CreateUpdateDepartmentDisciplineDto, CreateUpdateDepartmentDisciplineDto>, IDepartmentDisciplineAppService, ITransientDependency, IValidationEnabled

    {
        public DepartmentDisciplineAppService(IRepository<DepartmentDiscipline.DepartmentDiscipline, Guid> repository) : base(repository)
        {
        }

 
    }
}