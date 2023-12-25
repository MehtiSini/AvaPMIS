using System;
using System.Threading.Tasks;
using AvaPMIS.Main.Company;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using static AvaPMIS.Main.Company.ICompanyAppService;

namespace AvaPMIS.Main.Controllers
{
    [Route("api/app")]
    public class CompanyController : AbpController
    {
        private readonly ICompanyAppService _companyAppService;

        public CompanyController(ICompanyAppService companyAppService)
        {
            _companyAppService = companyAppService;
        }



        #region Admin

        [HttpPost]
        [Route("admin/company")]
        [Authorize(Roles = "Admins,Operators")]
        public async Task<CompanyDto> Create([FromBody] CreateUpdateCompanyDto objectDto)
        {
            var res = await _companyAppService.CreateAsync(objectDto);
            return res;
        }


        [HttpDelete]
        [Route("admin/company")]
        [Authorize(Roles = "Admins,Operators")]
        public async Task<bool> Delete(Guid id)
        {
            await _companyAppService.DeleteAsync(id);
            return true;
        }

        [HttpPut]
        [Route("admin/company")]
        [Authorize(Roles = "Admins,Operators")]
        public async Task<CompanyDto> Update(Guid id, [FromBody] CreateUpdateCompanyDto objDto)
        {
            var res = await _companyAppService.UpdateAsync(id, objDto);
            return res;
        }

        #endregion

        #region Public

        [HttpGet]
        [Route("public/company")]
        public async Task<PagedResultDto<CompanyDto>> GetListAsync([FromQuery] PagedAndSortedResultRequestDto input)
        {
            var res = await _companyAppService.GetListAsync(input);
            return res;

        }

        [HttpGet]
        [Route("public/getdefs")]
        public async Task<PagedResultDto<CombinedDataDto>> GetDefs([FromQuery] PagedAndSortedResultRequestDto input)
        {
            var res = await _companyAppService.GetAllDefs(input);
            return res;

        }

        #endregion
    }
}
