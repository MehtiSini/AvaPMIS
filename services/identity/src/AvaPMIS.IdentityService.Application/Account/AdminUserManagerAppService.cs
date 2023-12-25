using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Internal.Mappers;
using AvaPMIS.IdentityService.Identity;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Localization;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;
using Volo.Abp.ObjectExtending;
using Volo.Abp.ObjectMapping;
using Volo.Abp.SettingManagement;
using Volo.Abp.Users;
using IdentityRole = Volo.Abp.Identity.IdentityRole;
using IdentityUser = Volo.Abp.Identity.IdentityUser;
using IdentityUserCreateDto = AvaPMIS.IdentityService.Identity.Dtos.IdentityUserCreateDto;
using IdentityUserCreateOrUpdateDtoBase = AvaPMIS.IdentityService.Identity.Dtos.IdentityUserCreateOrUpdateDtoBase;
using IdentityUserUpdateDto = AvaPMIS.IdentityService.Identity.Dtos.IdentityUserUpdateDto;
using IIdentityRoleRepository = AvaPMIS.IdentityService.Identity.IIdentityRoleRepository;
using IIdentityUserRepository = AvaPMIS.IdentityService.Identity.IIdentityUserRepository;

namespace AvaPMIS.IdentityService.Account
{
    

    public class AdminUserManagerAppService : ApplicationService, IAdminUserManagerAppService, ITransientDependency
    {
        protected IdentityUserManager UserManager { get; }
        protected IdentityRoleManager RoleManager { get; }
        protected IIdentityUserRepository UserRepository { get; }
        protected IIdentityRoleRepository RoleRepository { get; }
        protected IOptions<IdentityOptions> IdentityOptions { get; }
        private readonly ISettingManager _settingManager;
        public AdminUserManagerAppService(
        IdentityUserManager userManager,
            IIdentityUserRepository userRepository,
        IIdentityRoleRepository roleRepository,
            IOptions<IdentityOptions> identityOptions, IdentityRoleManager roleManager)
        {
            UserManager = userManager;
            UserRepository = userRepository;
            RoleRepository = roleRepository;
            IdentityOptions = identityOptions;
            RoleManager = roleManager;
            ObjectMapperContext = typeof(AbpIdentityApplicationModule);
            LocalizationResource = typeof(IdentityResource);
        }

        [Route("/api/account/admin/get/{id}")]
        public virtual async Task<IdentityUserDto> GetAsync(Guid id)
        {
            var res = await UserManager.GetByIdAsync(id);
            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(
                res
            );
        }

        [Route("/api/account/admin/getList")]
        public virtual async Task<PagedResultDto<IdentityUserDto>> GetListAsync([FromQuery] GetIdentityUsersInput input)
        {
            var count = await UserRepository.GetCountAsync(input.Filter);
            var list = await UserRepository.GetListAsync(input.Sorting, input.MaxResultCount, input.SkipCount, input.Filter);

            return new PagedResultDto<IdentityUserDto>(
                count,
                ObjectMapper.Map<List<IdentityUser>, List<IdentityUserDto>>(list)
            );
        }
        [Route("/api/account/admin/getAdminList")]
        public virtual async Task<PagedResultDto<IdentityUserDto>> GetAdminListAsync([FromQuery] GetIdentityUsersInput input)
        {
            var role =await RoleManager.FindByNameAsync(IdentityPermissionsConsts.Roles.Admin);
            var count = await UserRepository.GetCountAsync(input.Filter, roleId: role.Id);
            var list = await UserRepository.GetListAsync(input.Sorting, input.MaxResultCount, input.SkipCount, input.Filter,roleId:role.Id);

            return new PagedResultDto<IdentityUserDto>(
                count,
                ObjectMapper.Map<List<IdentityUser>, List<IdentityUserDto>>(list)
            );
        }
        [Route("/api/account/admin/getOperatorList")]
        public virtual async Task<PagedResultDto<IdentityUserDto>> GetOperatorListAsync([FromQuery]GetIdentityUsersInput input)
        {
            var role = await RoleManager.FindByNameAsync(IdentityPermissionsConsts.Roles.Operator);
            var count = await UserRepository.GetCountAsync(input.Filter, roleId: role.Id);
            var list = await UserRepository.GetListAsync(input.Sorting, input.MaxResultCount, input.SkipCount, input.Filter, roleId: role.Id);

            return new PagedResultDto<IdentityUserDto>(
                count,
                ObjectMapper.Map<List<IdentityUser>, List<IdentityUserDto>>(list)
            );
        }
        [Route("/api/account/admin/getRoles/{id}")]
        public virtual async Task<ListResultDto<IdentityRoleDto>> GetRolesAsync(Guid id)
        {
            //TODO: Should also include roles of the related OUs.

            var roles = await UserRepository.GetRolesAsync(id);

            return new ListResultDto<IdentityRoleDto>(
                ObjectMapper.Map<List<IdentityRole>, List<IdentityRoleDto>>(roles)
            );
        }

        [Route("/api/account/admin/getAssignableRoles")]
        public virtual async Task<ListResultDto<IdentityRoleDto>> GetAssignableRolesAsync()
        {
            var list = await RoleRepository.GetListAsync();
            return new ListResultDto<IdentityRoleDto>(
                ObjectMapper.Map<List<IdentityRole>, List<IdentityRoleDto>>(list));
        }

        [Route("/api/account/admin/getAssignableRoles")]
        public async Task<IdentityUserDto> CreateAsync([FromQuery] IdentityUserCreateDto input, string roleName)
        {
            await IdentityOptions.SetAsync();

            var user = new IdentityUser(
                GuidGenerator.Create(),
                input.UserName,
                input.Email,
                CurrentTenant.Id
            );

            input.MapExtraPropertiesTo(user);

            (await UserManager.CreateAsync(user, input.Password)).CheckErrors();
            await UpdateUserByInput(user, input);
            (await UserManager.UpdateAsync(user)).CheckErrors();
            if (!string.IsNullOrEmpty(roleName))
                (await UserManager.AddToRoleAsync(user,roleName)).CheckErrors();
            else
            {
                await UserManager.AddDefaultRolesAsync(user);
            }

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(user);
        }

        //[Authorize(Roles = IdentityPermissionsConsts.Roles.Admin)]
        [Route("/api/account/admin/createAdmin")]
        [Authorize(Roles = IdentityPermissionsConsts.Roles.Admin)]
        public virtual async Task<IdentityUserDto> CreateAdminAsync(IdentityUserCreateDto input)
        {
            return await CreateAsync(input, IdentityPermissionsConsts.Roles.Admin);
        }
        [Route("/api/account/admin/createOperator")]
        public virtual async Task<IdentityUserDto> CreateOperatorAsync(IdentityUserCreateDto input)
        {
            return await CreateAsync(input, IdentityPermissionsConsts.Roles.Operator);
        }
        [Route("/api/account/admin/update")]
        public virtual async Task<IdentityUserDto> UpdateAsync(Guid id, IdentityUserUpdateDto input)
        {
            await IdentityOptions.SetAsync();

            var user = await UserManager.GetByIdAsync(id);

            user.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);

            (await UserManager.SetUserNameAsync(user, input.UserName)).CheckErrors();

            await UpdateUserByInput(user, input);
            input.MapExtraPropertiesTo(user);

            (await UserManager.UpdateAsync(user)).CheckErrors();

            if (!input.Password.IsNullOrEmpty())
            {
                (await UserManager.RemovePasswordAsync(user)).CheckErrors();
                (await UserManager.AddPasswordAsync(user, input.Password)).CheckErrors();
            }

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(user);
        }

        [Route("/api/account/admin/delete")]
        public virtual async Task DeleteAsync(Guid id)
        {
            if (CurrentUser.Id == id)
            {
                throw new BusinessException(code: IdentityErrorCodes.UserSelfDeletion);
            }

            var user = await UserManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return;
            }

            (await UserManager.DeleteAsync(user)).CheckErrors();
        }


        public virtual async Task UpdateRolesAsync(Guid id, IdentityUserUpdateRolesDto input)
        {
            var user = await UserManager.GetByIdAsync(id);
            (await UserManager.SetRolesAsync(user, input.RoleNames)).CheckErrors();
            await UserRepository.UpdateAsync(user);
        }

        [Route("/api/account/admin/findByUsername")]
        public virtual async Task<IdentityUserDto> FindByUsernameAsync(string userName)
        {
            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(
                await UserManager.FindByNameAsync(userName)
            );
        }

        [Route("/api/account/admin/findByEmail")]
        public virtual async Task<IdentityUserDto> FindByEmailAsync(string email)
        {
            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(
                await UserManager.FindByEmailAsync(email)
            );
        }

        [Route("/api/account/admin/findByPhoneNumber")]
        public virtual async Task<IdentityUserDto> FindByPhoneAsync(string phoneNumber)
        {
            var res = await UserRepository.FindByPhoneNumberAsync(phoneNumber, null);
            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(
                res
            );
        }

        public virtual async Task UpdateUserByInput(IdentityUser user, IdentityUserCreateOrUpdateDtoBase input)
        {
            if (!string.Equals(user.Email, input.Email, StringComparison.InvariantCultureIgnoreCase))
            {
                (await UserManager.SetEmailAsync(user, input.Email)).CheckErrors();
            }

            if (!string.Equals(user.PhoneNumber, input.PhoneNumber, StringComparison.InvariantCultureIgnoreCase))
            {
                (await UserManager.SetPhoneNumberAsync(user, input.PhoneNumber)).CheckErrors();
            }

            if (user.Id != CurrentUser.Id)
            {
                (await UserManager.SetLockoutEnabledAsync(user, input.LockoutEnabled)).CheckErrors();
            }

            user.Name = input.Name;
            user.Surname = input.Surname;
            (await UserManager.UpdateAsync(user)).CheckErrors();
            user.SetIsActive(input.IsActive);
            //if (input.RoleNames != null)
            //{
            //    (await UserManager.SetRolesAsync(user, input.RoleNames)).CheckErrors();
            //}
        }
    }
}
