using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using AvaPMIS.IdentityService.Account;
using AvaPMIS.IdentityService.Identity;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;
using IdentityUserCreateDto = AvaPMIS.IdentityService.Identity.Dtos.IdentityUserCreateDto;
using IdentityUserUpdateDto = AvaPMIS.IdentityService.Identity.Dtos.IdentityUserUpdateDto;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Users;

namespace AvaPMIS.IdentityService.Controllers
{

    [Route("api/account")]
    public class AccountController: AbpController,ITransientDependency
    {
        IPhoneNumberLoginAccountAppService _phoneNumberLoginAccountAppService;
        IAdminUserManagerAppService _adminUserManagerAppService;
        private readonly ICurrentUser _currentUser;
        public AccountController(IPhoneNumberLoginAccountAppService phoneNumberLoginAccountAppService, IAdminUserManagerAppService adminUserManagerAppService, ICurrentUser currentUser)
        {
            _phoneNumberLoginAccountAppService = phoneNumberLoginAccountAppService;
            _adminUserManagerAppService = adminUserManagerAppService;
            _currentUser = currentUser;
        }

        

        #region Admin

        [HttpPost]
        [Route("admin/createAdmin")]
        [Authorize(Roles = "Admins")]
        public virtual async Task<IdentityUserDto> CreateAdminAsync([FromBody] IdentityUserCreateDto input)
        {

            var user = _currentUser;
            //var admin = _currentUser.Roles;


            return await _adminUserManagerAppService.CreateAdminAsync(input);
        }
        [HttpPost]
        [Route("admin/createOperator")]
        [Authorize(Roles = "Admins")]
        public virtual async Task<IdentityUserDto> CreateOperatorAsync([FromBody] IdentityUserCreateDto input)
        {
            return await _adminUserManagerAppService.CreateOperatorAsync(input);
        }
        [HttpGet]
        [Route("admin/{id}")]
        [Authorize(Roles = "Admins,Operators")]
        public virtual async Task<IdentityUserDto> GetAsync(Guid id)
        {
            return await _adminUserManagerAppService.GetAsync(id);
        }
        [HttpPost]
        [Route("admin/getList")]
        [Authorize(Roles = "Admins,Operators")]
        public async virtual Task<PagedResultDto<IdentityUserDto>> GetListAsync([FromBody] GetIdentityUsersInput input)
        {
            return await _adminUserManagerAppService.GetListAsync(input);
        }
        [HttpPost]
        [Route("admin/getAdminList")]
        [Authorize(Roles = "Admins")]
        public async virtual Task<PagedResultDto<IdentityUserDto>> GetAdminListAsync([FromBody] GetIdentityUsersInput input)
        {
            return await _adminUserManagerAppService.GetAdminListAsync(input);
        }
        [HttpPost]
        [Route("admin/getOperatorList")]
        [Authorize(Roles = "Admins")]
        public async virtual Task<PagedResultDto<IdentityUserDto>> GetOperatorListAsync([FromBody] GetIdentityUsersInput input)
        {
            return await _adminUserManagerAppService.GetOperatorListAsync(input);
        }
        [HttpPut]
        [Route("admin/{id}")]
        [Authorize(Roles = "Admins,Operators")]
        public async virtual Task<IdentityUserDto> UpdateAsync(Guid id, [FromBody] IdentityUserUpdateDto input)
        {
            return await _adminUserManagerAppService.UpdateAsync(id,input);
        }
        [HttpDelete]
        [Route("admin/{id}")]
        [Authorize(Roles = "Admins,Operators")]
        public async virtual Task<bool> DeleteAsync(Guid id)
        {
            var res = false;
            await _adminUserManagerAppService.DeleteAsync(id);
            res = true;
            return res;
        }
        [HttpGet]
        [Route("admin/findByUsername/{userName}")]
        [Authorize(Roles = "Admins,Operators")]
        public async virtual Task<IdentityUserDto> FindByUsernameAsync(string userName)
        {
            return await _adminUserManagerAppService.FindByUsernameAsync(userName);
        }

        [HttpGet]
        [Route("admin/findByPhoneNumber/{phoneNumber}")]
        [Authorize(Roles = "Admins,Operators")]
        public async virtual Task<IdentityUserDto> FindByPhoneAsync(string phoneNumber)
        {
            return await _adminUserManagerAppService.FindByPhoneAsync(phoneNumber);
        }

        #endregion

        #region Public

        [HttpGet]
        [Route("user")]
        [Authorize()]
        public virtual async Task<IdentityUserDto> GetUser()
        {
            var userName = _currentUser.UserName;
            return await _adminUserManagerAppService.FindByUsernameAsync(userName);
        }
        [HttpPut]
        [Route("user")]
        [Authorize()]
        public async virtual Task<IdentityUserDto> UpdateUserAsync([FromBody] IdentityUserUpdateDto input)
        {
            var userId = _currentUser.Id;
            return await _adminUserManagerAppService.UpdateAsync(userId.Value, input);
        }
  

        #endregion

    }
}
