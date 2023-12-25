using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AvaPMIS.Main.Company;
using AvaPMIS.Main.Department;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Nozhan.Accounting.DepartmentAgg
{
    public interface IDepartmentAppService : ICrudAppService<
       DepartmentDto,
       Guid,
       PagedAndSortedResultRequestDto,
       CreateUpdateDepartmentDto,
       CreateUpdateDepartmentDto>

    {
        Task<PagedResultDto<DepartmentDto>> GetDepartments(PagedAndSortedResultRequestDto input);

    }
}
