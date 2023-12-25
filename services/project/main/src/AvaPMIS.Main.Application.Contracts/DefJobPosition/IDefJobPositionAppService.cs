using AvaPMIS.Main.DefDiscipline;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace AvaPMIS.Main.DefJobPosition
{
    public interface IDefJobPositionAppService : ICrudAppService<
       DefJobPositionDto,
       Guid,
       PagedAndSortedResultRequestDto,
       CreateUpdateJobPositionDto,
       CreateUpdateJobPositionDto>

    {

    }
}

