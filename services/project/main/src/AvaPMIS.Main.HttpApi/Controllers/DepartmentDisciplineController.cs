using System;
using System.Threading.Tasks;
using AvaPMIS.Main.DepartmentDiscipline;
using AvaPMIS.Main.Discipline;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace AvaPMIS.Main.Controllers
{
    [Route("api/app")]
    public class DepartmentDisciplineController : AbpController
    {
        private readonly IDepartmentDisciplineAppService _departmentDisciplineAppService;

        public DepartmentDisciplineController(IDepartmentDisciplineAppService departmentdisciplineAppService)
        {
            _departmentDisciplineAppService = departmentdisciplineAppService;
        }


        #region Admin

        [HttpPost]
        [Route("admin/DepartmentDiscipline")]
        [Authorize(Roles = "Admins,Operators")]
        public async Task<DepartmentDisciplineDto> Create([FromBody] CreateUpdateDepartmentDisciplineDto objectDto)
        {
            var res = await _departmentDisciplineAppService.CreateAsync(objectDto);
            return res;
        }


        [HttpDelete]
        [Route("admin/DepartmentDiscipline")]
        [Authorize(Roles = "Admins,Operators")]
        public async Task<bool> Delete(Guid id)
        {
            await _departmentDisciplineAppService.DeleteAsync(id);
            return true;
        }

        [HttpPut]
        [Route("admin/DepartmentDiscipline")]
        [Authorize(Roles = "Admins,Operators")]
        public async Task<DepartmentDisciplineDto> Update(Guid id, [FromBody] CreateUpdateDepartmentDisciplineDto objDto)
        {
            var res = await _departmentDisciplineAppService.UpdateAsync(id, objDto);
            return res;
        }

        #endregion

        #region Public

        [HttpGet]
        [Route("public/DepartmentDiscipline")]
        public async Task<PagedResultDto<DepartmentDisciplineDto>> GetList([FromQuery] PagedAndSortedResultRequestDto input)
        {
            var res = await _departmentDisciplineAppService.GetListAsync(input);
            return res;

        }


        #endregion
    }
}
