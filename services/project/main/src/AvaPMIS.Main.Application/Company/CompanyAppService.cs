using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Validation;

namespace AvaPMIS.Main.Company
{
    public class CompanyAppService : CrudAppService<Company, CompanyDto, Guid, PagedAndSortedResultRequestDto,
        CreateUpdateCompanyDto, CreateUpdateCompanyDto>, ICompanyAppService, ITransientDependency, IValidationEnabled

    {
        public CompanyAppService(IRepository<Company, Guid> repository) : base(repository)
        {
        }

        public Task<PagedResultDto<CompanyDto>> GetCompanies(PagedAndSortedResultRequestDto input)
        {
            throw new NotImplementedException();
        }
    }
}
