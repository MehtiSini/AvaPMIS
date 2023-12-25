using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace AvaPMIS.Main.DisciplineJobPosition
{
    public interface IDisciplineJobPositionAppService : ICrudAppService<
       DisciplineJobPositionDto,
       Guid,
       PagedAndSortedResultRequestDto,
       CreateUpdateDisciplineJobPositionDto,
       CreateUpdateDisciplineJobPositionDto>

    {

    }
}