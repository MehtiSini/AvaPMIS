using System;
using System.Threading.Tasks;
using AvaPMIS.Main.DefDepartment;
using AvaPMIS.Main.DefDiscipline;
using AvaPMIS.Main.DefJobPosition;
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

        Task<PagedResultDto<CombinedDataDto>> GetAllDefs(PagedAndSortedResultRequestDto input);

        public class CombinedDataDto
        {
            public PagedResultDto<DefDepartmentDto> DefDepartments { get; set; }
            public PagedResultDto<DefJobPositionDto> DefJobPositions{ get; set; }
            public PagedResultDto<DefDisciplineDto> DefDisciplines{ get; set; }
        }

    }
}
