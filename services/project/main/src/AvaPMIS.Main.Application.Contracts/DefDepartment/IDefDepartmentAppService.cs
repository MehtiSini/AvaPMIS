using AvaPMIS.Main.Company;
using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace AvaPMIS.Main.DefDepartment
{
    public interface IDefDepartmentAppService : ICrudAppService<
       DefDepartmentDto,
       Guid,
       PagedAndSortedResultRequestDto,
       CreateUpdateDefDepartmentDto,
       CreateUpdateDefDepartmentDto>

    {

    }
}

