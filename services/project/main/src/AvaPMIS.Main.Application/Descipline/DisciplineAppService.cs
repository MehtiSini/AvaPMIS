using AvaPMIS.Main.Department;
using Nozhan.Accounting.DepartmentAgg;
using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Validation;
using AvaPMIS.Main.Discipline;

namespace AvaPMIS.Main.Descipline
{
    public class DisciplineAppService : CrudAppService<Discipline.Discipline, DisciplineDto, Guid, PagedAndSortedResultRequestDto,
        CreateUpdateDisciplineDto, CreateUpdateDisciplineDto>, IDisciplineAppService, ITransientDependency, IValidationEnabled

    {
        public DisciplineAppService(IRepository<Discipline.Discipline, Guid> repository) : base(repository)
        {
        }

 
    }
}