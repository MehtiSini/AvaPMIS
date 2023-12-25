using System;
using System.Threading.Tasks;
using AvaPMIS.Main.Company;
using AvaPMIS.Main.DefJobPosition;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace AvaPMIS.Main.Controllers
{
    [Route("api/app")]
    public class DefJobPositionController : AbpController
    {
        private readonly IDefJobPositionAppService _jobPositionAppService;

        public DefJobPositionController(IDefJobPositionAppService jobPositionAppService)
        {
            _jobPositionAppService = jobPositionAppService;
        }


        #region Admin

        [HttpPost]
        [Route("admin/defjobposition")]
        [Authorize(Roles = "Admins,Operators")]
        public async Task<DefJobPositionDto> Create([FromBody] CreateUpdateJobPositionDto objectDto)
        {
            var res = await _jobPositionAppService.CreateAsync(objectDto);
            return res;
        }


        [HttpDelete]
        [Route("admin/defjobposition")]
        [Authorize(Roles = "Admins,Operators")]
        public async Task<bool> Delete(Guid id)
        {
            await _jobPositionAppService.DeleteAsync(id);
            return true;
        }

        [HttpPut]
        [Route("admin/defjobposition")]
        [Authorize(Roles = "Admins,Operators")]
        public async Task<DefJobPositionDto> Update(Guid id, [FromBody] CreateUpdateJobPositionDto objDto)
        {
            var res = await _jobPositionAppService.UpdateAsync(id, objDto);
            return res;
        }

        #endregion

        #region Public

        [HttpGet]
        [Route("public/defjobposition")]
        public async Task<PagedResultDto<DefJobPositionDto>> GetListAsync([FromQuery] PagedAndSortedResultRequestDto input)
        {
            var res = await _jobPositionAppService.GetListAsync(input);
            return res;

        }
        #endregion
    }
}
