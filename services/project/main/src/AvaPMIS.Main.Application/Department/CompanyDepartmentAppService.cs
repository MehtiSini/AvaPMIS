using AvaPMIS.Main.Company;
using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Validation;
using Nozhan.Accounting.DepartmentAgg;

namespace AvaPMIS.Main.CompanyDepartment
{
    public class CompanyDepartmentAppService : CrudAppService<CompanyDepartment, CompanyDepartmentDto, Guid, PagedAndSortedResultRequestDto,
        CreateUpdateCompanyDepartmentDto, CreateUpdateCompanyDepartmentDto>, ICompanyDepartmentAppService, ITransientDependency, IValidationEnabled

    {
        public CompanyDepartmentAppService(IRepository<CompanyDepartment, Guid> repository) : base(repository)
        {
        }

        public Task<PagedResultDto<CompanyDepartmentDto>> GetCompanyDepartments(PagedAndSortedResultRequestDto input)
        {
            throw new NotImplementedException();
        }
    }
}