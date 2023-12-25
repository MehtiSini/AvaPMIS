using System;
using System.Threading.Tasks;
using AvaPMIS.Main.Department;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nozhan.Accounting.DepartmentAgg;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace AvaPMIS.Main.Controllers
{
    [Route("api/app")]
    public class DepartmentController : AbpController
    {
        private readonly IDepartmentAppService _departmentAppService;

        public DepartmentController(IDepartmentAppService departmentAppService)
        {
            _departmentAppService = departmentAppService;
        }

        #region Admin

        [HttpPost]
        [Route("admin/department")]
        [Authorize(Roles = "Admins,Operators")]
        public async Task<DepartmentDto> Create([FromBody] CreateUpdateDepartmentDto objectDto)
        {
            var res = await _departmentAppService.CreateAsync(objectDto);
            return res;
        }


        [HttpDelete]
        [Route("admin/department")]
        [Authorize(Roles = "Admins,Operators")]
        public async Task<bool> Delete(Guid id)
        {
            await _departmentAppService.DeleteAsync(id);
            return true;
        }

        [HttpPut]
        [Route("admin/department")]
        [Authorize(Roles = "Admins,Operators")]
        public async Task<DepartmentDto> Update(Guid id, [FromBody] CreateUpdateDepartmentDto objDto)
        {
            var res = await _departmentAppService.UpdateAsync(id, objDto);
            return res;
        }

        #endregion

        #region Public

        [HttpGet]
        [Route("public/department")]
        public async Task<PagedResultDto<DepartmentDto>> GetDepartments([FromQuery] PagedAndSortedResultRequestDto input)
        {
            var res = await _departmentAppService.GetListAsync(input);
            return res;

        }


        #endregion
    }
}
