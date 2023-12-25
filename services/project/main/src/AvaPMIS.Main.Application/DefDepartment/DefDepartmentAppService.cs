using AvaPMIS.Main.Company;
using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Validation;

namespace AvaPMIS.Main.DefDepartment
{
    public class DefDepartmentAppService : CrudAppService<DefDepartment, DefDepartmentDto, Guid, PagedAndSortedResultRequestDto,
        CreateUpdateDefDepartmentDto, CreateUpdateDefDepartmentDto>, IDefDepartmentAppService, ITransientDependency, IValidationEnabled

    {
        public DefDepartmentAppService(IRepository<DefDepartment, Guid> repository) : base(repository)
        {
        }
    }
}
