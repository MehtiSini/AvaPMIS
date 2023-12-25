using AvaPMIS.Main.Department;
using Nozhan.Accounting.DepartmentAgg;
using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Validation;

namespace AvaPMIS.Main.JobPosition
{
    public class JobPositionAppService : CrudAppService<JobPosition, JobPositionDto, Guid, PagedAndSortedResultRequestDto,
        CreateUpdateJobPositionDto, CreateUpdateJobPositionDto>, IJobPositionAppService, ITransientDependency, IValidationEnabled

    {
        public JobPositionAppService(IRepository<JobPosition, Guid> repository) : base(repository)
        {
        }

       
    }
}