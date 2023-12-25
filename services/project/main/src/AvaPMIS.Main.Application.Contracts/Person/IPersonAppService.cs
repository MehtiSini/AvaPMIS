using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace AvaPMIS.Main.Person
{
    public interface IPersonAppService : ICrudAppService<
       PersonDto,
       Guid,
       PagedAndSortedResultRequestDto,
       CreateUpdatePersonDto,
       CreateUpdatePersonDto>

    {

    }
}