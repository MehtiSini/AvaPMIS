using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace AvaPMIS.Main.Company
{
    public interface ICompanyAppService : ICrudAppService< 
       CompanyDto, 
       Guid, 
       PagedAndSortedResultRequestDto,
       CreateUpdateCompanyDto, 
       CreateUpdateCompanyDto> 

    {
        Task<PagedResultDto<CompanyDto>> GetCompanies(PagedAndSortedResultRequestDto input);

    }
}
