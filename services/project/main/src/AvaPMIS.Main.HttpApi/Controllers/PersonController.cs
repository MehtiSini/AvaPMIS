using System;
using System.Threading.Tasks;
using AvaPMIS.Main.Person;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace AvaPMIS.Main.Controllers
{
    [Route("api/app")]
    public class PersonController : AbpController
    {
        private readonly IPersonAppService _personAppService;

        public PersonController(IPersonAppService personAppService)
        {
            _personAppService = personAppService;
        }

        #region Admin

        [HttpPost]
        [Route("admin/person")]
        [Authorize(Roles = "Admins,Operators")]
        public async Task<PersonDto> Create([FromBody] CreateUpdatePersonDto objectDto)
        {
            var res = await _personAppService.CreateAsync(objectDto);
            return res;
        }


        [HttpDelete]
        [Route("admin/person")]
        [Authorize(Roles = "Admins,Operators")]
        public async Task<bool> Delete(Guid id)
        {
            await _personAppService.DeleteAsync(id);
            return true;
        }

        [HttpPut]
        [Route("admin/person")]
        [Authorize(Roles = "Admins,Operators")]
        public async Task<PersonDto> Update(Guid id, [FromBody] CreateUpdatePersonDto objDto)
        {
            var res = await _personAppService.UpdateAsync(id, objDto);
            return res;
        }

        #endregion

        #region Public

        [HttpGet]
        [Route("public/person")]
        public async Task<PagedResultDto<PersonDto>> GetListAsync([FromQuery] PagedAndSortedResultRequestDto input)
        {
            var res = await _personAppService.GetListAsync(input);
            return res;

        }
        #endregion
    }
}
