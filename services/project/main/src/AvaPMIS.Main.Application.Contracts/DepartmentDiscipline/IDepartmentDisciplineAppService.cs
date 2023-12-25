using System;
using AvaPMIS.Main.Discipline;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace AvaPMIS.Main.DepartmentDiscipline
{
    public interface IDepartmentDisciplineAppService : ICrudAppService<
       DepartmentDisciplineDto,
       Guid,
       PagedAndSortedResultRequestDto,
       CreateUpdateDepartmentDisciplineDto,
       CreateUpdateDepartmentDisciplineDto>

    {

    }
}
