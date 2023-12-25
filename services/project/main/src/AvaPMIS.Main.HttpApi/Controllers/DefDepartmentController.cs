using System;
using System.Threading.Tasks;
using AvaPMIS.Main.DefDepartment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace AvaPMIS.Main.Controllers
{
    [Route("api/app")]
    public class DefDepartmentController : AbpController
    {
        private readonly IDefDepartmentAppService _departmentAppService;

        public DefDepartmentController(IDefDepartmentAppService departmentAppService)
        {
            _departmentAppService = departmentAppService;
        }

        #region Admin

        [HttpPost]
        [Route("admin/defdepartment")]
        [Authorize(Roles = "Admins,Operators")]
        public async Task<DefDepartmentDto> Create([FromBody] CreateUpdateDefDepartmentDto objectDto)
        {
            var res = await _departmentAppService.CreateAsync(objectDto);
            return res;
        }


        [HttpDelete]
        [Route("admin/defdepartment")]
        [Authorize(Roles = "Admins,Operators")]
        public async Task<bool> Delete(Guid id)
        {
            await _departmentAppService.DeleteAsync(id);
            return true;
        }

        [HttpPut]
        [Route("admin/defdepartment")]
        [Authorize(Roles = "Admins,Operators")]
        public async Task<DefDepartmentDto> Update(Guid id, [FromBody] CreateUpdateDefDepartmentDto objDto)
        {
            var res = await _departmentAppService.UpdateAsync(id, objDto);
            return res;
        }

        #endregion

        #region Public

        [HttpGet]
        [Route("public/defdepartment")]
        public async Task<PagedResultDto<DefDepartmentDto>> GetListAsync([FromQuery] PagedAndSortedResultRequestDto input)
        {
            var res = await _departmentAppService.GetListAsync(input);
            return res;

        }
        #endregion
    }
}
