using System;
using System.Threading.Tasks;
using AvaPMIS.Main.CompanyDepartment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nozhan.Accounting.DepartmentAgg;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace AvaPMIS.Main.Controllers
{
    [Route("api/app")]
    public class CompanyDepartmentController : AbpController
    {
        private readonly ICompanyDepartmentAppService _companyDepartmentAppService;

        public CompanyDepartmentController(ICompanyDepartmentAppService departmentAppService)
        {
            _companyDepartmentAppService = departmentAppService;
        }

        #region Admin

        [HttpPost]
        [Route("admin/CompanyDepartment")]
        [Authorize(Roles = "Admins,Operators")]
        public async Task<CompanyDepartmentDto> Create([FromBody] CreateUpdateCompanyDepartmentDto objectDto)
        {
            var res = await _companyDepartmentAppService.CreateAsync(objectDto);
            return res;
        }


        [HttpDelete]
        [Route("admin/CompanyDepartment")]
        [Authorize(Roles = "Admins,Operators")]
        public async Task<bool> Delete(Guid id)
        {
            await _companyDepartmentAppService.DeleteAsync(id);
            return true;
        }

        [HttpPut]
        [Route("admin/CompanyDepartment")]
        [Authorize(Roles = "Admins,Operators")]
        public async Task<CompanyDepartmentDto> Update(Guid id, [FromBody] CreateUpdateCompanyDepartmentDto objDto)
        {
            var res = await _companyDepartmentAppService.UpdateAsync(id, objDto);
            return res;
        }

        #endregion

        #region Public

        [HttpGet]
        [Route("public/CompanyDepartment")]
        public async Task<PagedResultDto<CompanyDepartmentDto>> GetList([FromQuery] PagedAndSortedResultRequestDto input)
        {
            var res = await _companyDepartmentAppService.GetListAsync(input);
            return res;

        }


        #endregion
    }
}
