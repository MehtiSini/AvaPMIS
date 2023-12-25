using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace AvaPMIS.Main.Discipline
{
    public interface IDisciplineAppService : ICrudAppService<
       DisciplineDto,
       Guid,
       PagedAndSortedResultRequestDto,
       CreateUpdateDisciplineDto,
       CreateUpdateDisciplineDto>

    {

    }
}
