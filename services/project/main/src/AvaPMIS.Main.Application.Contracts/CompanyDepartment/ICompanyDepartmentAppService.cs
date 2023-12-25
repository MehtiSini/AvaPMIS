using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AvaPMIS.Main.Company;
using AvaPMIS.Main.CompanyDepartment;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Nozhan.Accounting.DepartmentAgg
{
    public interface ICompanyDepartmentAppService : ICrudAppService<
       CompanyDepartmentDto,
       Guid,
       PagedAndSortedResultRequestDto,
       CreateUpdateCompanyDepartmentDto,
       CreateUpdateCompanyDepartmentDto>

    {
        Task<PagedResultDto<CompanyDepartmentDto>> GetCompanyDepartments(PagedAndSortedResultRequestDto input);

    }
}
