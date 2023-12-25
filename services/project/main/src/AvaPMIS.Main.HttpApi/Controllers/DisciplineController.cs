using System;
using System.Threading.Tasks;
using AvaPMIS.Main.Discipline;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace AvaPMIS.Main.Controllers
{
    [Route("api/app")]
    public class DisciplineController : AbpController
    {
        private readonly IDisciplineAppService _disciplineAppService;

        public DisciplineController(IDisciplineAppService disciplineAppService)
        {
            _disciplineAppService = disciplineAppService;
        }


        #region Admin

        [HttpPost]
        [Route("admin/Discipline")]
        [Authorize(Roles = "Admins,Operators")]
        public async Task<DisciplineDto> Create([FromBody] CreateUpdateDisciplineDto objectDto)
        {
            var res = await _disciplineAppService.CreateAsync(objectDto);
            return res;
        }


        [HttpDelete]
        [Route("admin/Discipline")]
        [Authorize(Roles = "Admins,Operators")]
        public async Task<bool> Delete(Guid id)
        {
            await _disciplineAppService.DeleteAsync(id);
            return true;
        }

        [HttpPut]
        [Route("admin/Discipline")]
        [Authorize(Roles = "Admins,Operators")]
        public async Task<DisciplineDto> Update(Guid id, [FromBody] CreateUpdateDisciplineDto objDto)
        {
            var res = await _disciplineAppService.UpdateAsync(id, objDto);
            return res;
        }

        #endregion

        #region Public

        [HttpGet]
        [Route("public/Discipline")]
        public async Task<PagedResultDto<DisciplineDto>> GetDisciplines([FromQuery] PagedAndSortedResultRequestDto input)
        {
            var res = await _disciplineAppService.GetListAsync(input);
            return res;

        }


        #endregion
    }
}
