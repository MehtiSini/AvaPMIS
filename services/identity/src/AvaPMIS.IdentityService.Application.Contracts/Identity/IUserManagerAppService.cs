using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;
using IdentityUserCreateDto = AvaPMIS.IdentityService.Identity.Dtos.IdentityUserCreateDto;
using IdentityUserCreateOrUpdateDtoBase = AvaPMIS.IdentityService.Identity.Dtos.IdentityUserCreateOrUpdateDtoBase;
using IdentityUserUpdateDto = AvaPMIS.IdentityService.Identity.Dtos.IdentityUserUpdateDto;


namespace AvaPMIS.IdentityService.Identity
{
    public interface IAdminUserManagerAppService
    {
        Task<IdentityUserDto> GetAsync(Guid id);
        Task<PagedResultDto<IdentityUserDto>> GetListAsync(GetIdentityUsersInput input);
        Task<PagedResultDto<IdentityUserDto>> GetAdminListAsync(GetIdentityUsersInput input);
        Task<PagedResultDto<IdentityUserDto>> GetOperatorListAsync(GetIdentityUsersInput input);
        Task<ListResultDto<IdentityRoleDto>> GetRolesAsync(Guid id);
        Task<ListResultDto<IdentityRoleDto>> GetAssignableRolesAsync();
        Task<IdentityUserDto> CreateAsync(IdentityUserCreateDto input,string roleName);
        Task<IdentityUserDto> CreateAdminAsync(IdentityUserCreateDto input);
        Task<IdentityUserDto> CreateOperatorAsync(IdentityUserCreateDto input);
        Task<IdentityUserDto> UpdateAsync(Guid id, IdentityUserUpdateDto input);
        Task DeleteAsync(Guid id);
        Task UpdateRolesAsync(Guid id, IdentityUserUpdateRolesDto input);
        Task<IdentityUserDto> FindByUsernameAsync(string userName);
        Task<IdentityUserDto> FindByEmailAsync(string email);
        Task<IdentityUserDto> FindByPhoneAsync(string phoneNumber);

    }
}