using AvaPMIS.Main.Company;
using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Validation;
using Nozhan.Accounting.DepartmentAgg;

namespace AvaPMIS.Main.Department
{
    public class DepartmentAppService : CrudAppService<Department, DepartmentDto, Guid, PagedAndSortedResultRequestDto,
        CreateUpdateDepartmentDto, CreateUpdateDepartmentDto>, IDepartmentAppService, ITransientDependency, IValidationEnabled

    {
        public DepartmentAppService(IRepository<Department, Guid> repository) : base(repository)
        {
        }

        public Task<PagedResultDto<DepartmentDto>> GetDepartments(PagedAndSortedResultRequestDto input)
        {
            throw new NotImplementedException();
        }
    }
}