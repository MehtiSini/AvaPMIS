using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Validation;
using AvaPMIS.Main.DefJobPosition;
using AvaPMIS.Main.DefDepartment;
using AvaPMIS.Main.DefDiscipline;
using static AvaPMIS.Main.Company.ICompanyAppService;
using System.Collections.Generic;

namespace AvaPMIS.Main.Company
{
    public class CompanyAppService : CrudAppService<Company, CompanyDto, Guid, PagedAndSortedResultRequestDto,
        CreateUpdateCompanyDto, CreateUpdateCompanyDto>, ICompanyAppService, ITransientDependency, IValidationEnabled

    {
        private readonly IDefJobPositionAppService _defJobPositionAppService;
        private readonly IDefDepartmentAppService _defDepartmentAppService;
        private readonly IDefDisciplineAppService _defDisciplineAppService ;


        public CompanyAppService(IRepository<Company, Guid> repository, IDefJobPositionAppService defJobPositionAppService, IDefDepartmentAppService defDepartmentAppService, IDefDisciplineAppService defDisciplineAppService) : base(repository)
        {
            _defJobPositionAppService = defJobPositionAppService;
            _defDepartmentAppService = defDepartmentAppService;
            _defDisciplineAppService = defDisciplineAppService;
        }

        public async Task<PagedResultDto<CombinedDataDto>> GetAllDefs(PagedAndSortedResultRequestDto input)
        {
            var defDepartments = await _defDepartmentAppService.GetListAsync(input);
            var defDisciplines = await _defDisciplineAppService.GetListAsync(input);
            var defJobPositions = await _defJobPositionAppService.GetListAsync(input);

            var combinedData = new CombinedDataDto
            {
                DefDepartments = defDepartments,
                DefDisciplines = defDisciplines,
                DefJobPositions = defJobPositions,
            };

            var pagedResult = new PagedResultDto<CombinedDataDto>
            {
                TotalCount = 1,
                Items = new List<CombinedDataDto> { combinedData }
            };

            return pagedResult;
        }

        public async Task<PagedResultDto<CompanyDto>> GetCompanies(PagedAndSortedResultRequestDto input)
        {
            throw new NotImplementedException();
        }
    }
}
