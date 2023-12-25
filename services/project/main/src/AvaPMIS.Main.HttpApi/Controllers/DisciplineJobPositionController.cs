using System;
using System.Threading.Tasks;
using AvaPMIS.Main.DisciplineJobPosition;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace AvaPMIS.Main.Controllers
{
    [Route("api/app")]
    public class DisciplineJobPositionController : AbpController
    {
        private readonly IDisciplineJobPositionAppService _disciplineJobPositionAppService;

        public DisciplineJobPositionController(IDisciplineJobPositionAppService disciplineJobPositionAppService)
        {
           _disciplineJobPositionAppService = disciplineJobPositionAppService;
        }


        #region Admin

        [HttpPost]
        [Route("admin/DisciplineJobPosition")]
        [Authorize(Roles = "Admins,Operators")]
        public async Task<DisciplineJobPositionDto> Create([FromBody] CreateUpdateDisciplineJobPositionDto objectDto)
        {
            var res = await _disciplineJobPositionAppService.CreateAsync(objectDto);
            return res;
        }


        [HttpDelete]
        [Route("admin/DisciplineJobPosition")]
        [Authorize(Roles = "Admins,Operators")]
        public async Task<bool> Delete(Guid id)
        {
            await _disciplineJobPositionAppService.DeleteAsync(id);
            return true;
        }

        [HttpPut]
        [Route("admin/DisciplineJobPosition")]
        [Authorize(Roles = "Admins,Operators")]
        public async Task<DisciplineJobPositionDto> Update(Guid id, [FromBody] CreateUpdateDisciplineJobPositionDto objDto)
        {
            var res = await _disciplineJobPositionAppService.UpdateAsync(id, objDto);
            return res;
        }

        #endregion

        #region Public

        [HttpGet]
        [Route("public/DisciplineJobPosition")]
        public async Task<PagedResultDto<DisciplineJobPositionDto>> GetListAsync([FromQuery] PagedAndSortedResultRequestDto input)
        {
            var res = await _disciplineJobPositionAppService.GetListAsync(input);
            return res;

        }
        #endregion
    }
}
