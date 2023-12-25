using System;
using System.Threading.Tasks;
using AvaPMIS.Main.JobPosition;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace AvaPMIS.Main.Controllers
{
    [Route("api/app")]
    public class JobPositionController : AbpController
    {
        private readonly IJobPositionAppService _jobPositionAppService;

        public JobPositionController(IJobPositionAppService jobPositionAppService)
        {
           _jobPositionAppService = jobPositionAppService;
        }


        #region Admin

        [HttpPost]
        [Route("admin/JobPosition")]
        [Authorize(Roles = "Admins,Operators")]
        public async Task<JobPositionDto> Create([FromBody] CreateUpdateJobPositionDto objectDto)
        {
            var res = await _jobPositionAppService.CreateAsync(objectDto);
            return res;
        }


        [HttpDelete]
        [Route("admin/JobPosition")]
        [Authorize(Roles = "Admins,Operators")]
        public async Task<bool> Delete(Guid id)
        {
            await _jobPositionAppService.DeleteAsync(id);
            return true;
        }

        [HttpPut]
        [Route("admin/JobPosition")]
        [Authorize(Roles = "Admins,Operators")]
        public async Task<JobPositionDto> Update(Guid id, [FromBody] CreateUpdateJobPositionDto objDto)
        {
            var res = await _jobPositionAppService.UpdateAsync(id, objDto);
            return res;
        }

        #endregion

        #region Public

        [HttpGet]
        [Route("public/JobPosition")]
        public async Task<PagedResultDto<JobPositionDto>> GetListAsync([FromQuery] PagedAndSortedResultRequestDto input)
        {
            var res = await _jobPositionAppService.GetListAsync(input);
            return res;

        }
        #endregion
    }
}
