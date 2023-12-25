using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace AvaPMIS.Main.JobPosition
{
    public interface IJobPositionAppService : ICrudAppService<
       JobPositionDto,
       Guid,
       PagedAndSortedResultRequestDto,
       CreateUpdateJobPositionDto,
       CreateUpdateJobPositionDto>

    {

    }
}