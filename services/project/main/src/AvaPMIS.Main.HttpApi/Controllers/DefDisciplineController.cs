using System;
using System.Threading.Tasks;
using AvaPMIS.Main.Company;
using AvaPMIS.Main.DefDiscipline;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace AvaPMIS.Main.Controllers
{
    [Route("api/app")]
    public class DefDisciplineController : AbpController
    {
        private readonly IDefDisciplineAppService _defDisciplineAppService;

   
        #region Admin

        [HttpPost]
        [Route("admin/defdiscipline")]
        [Authorize(Roles = "Admins,Operators")]
        public async Task<DefDisciplineDto> Create([FromBody] CreateUpdateDefDisciplineDto objectDto)
        {
            var res = await _defDisciplineAppService.CreateAsync(objectDto);
            return res;
        }


        [HttpDelete]
        [Route("admin/defdiscipline")]
        [Authorize(Roles = "Admins,Operators")]
        public async Task<bool> Delete(Guid id)
        {
            await _defDisciplineAppService.DeleteAsync(id);
            return true;
        }

        [HttpPut]
        [Route("admin/defdiscipline")]
        [Authorize(Roles = "Admins,Operators")]
        public async Task<DefDisciplineDto> Update(Guid id, [FromBody] CreateUpdateDefDisciplineDto objDto)
        {
            var res = await _defDisciplineAppService.UpdateAsync(id, objDto);
            return res;
        }

        #endregion

        #region Public

        [HttpGet]
        [Route("public/defdiscipline")]
        public async Task<PagedResultDto<DefDisciplineDto>> GetListAsync([FromQuery] PagedAndSortedResultRequestDto input)
        {
            var res = await _defDisciplineAppService.GetListAsync(input);
            return res;

        }
        #endregion
    }
}
