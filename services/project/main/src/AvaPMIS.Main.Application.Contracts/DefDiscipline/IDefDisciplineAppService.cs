using AvaPMIS.Main.DefDepartment;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace AvaPMIS.Main.DefDiscipline
{
    public interface IDefDisciplineAppService : ICrudAppService<
       DefDisciplineDto,
       Guid,
       PagedAndSortedResultRequestDto,
       CreateUpdateDefDisciplineDto,
       CreateUpdateDefDisciplineDto>

    {

    }
}
