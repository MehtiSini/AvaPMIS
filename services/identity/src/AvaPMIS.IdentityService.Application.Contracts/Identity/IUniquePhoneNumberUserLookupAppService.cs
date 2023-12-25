using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Users;

namespace AvaPMIS.IdentityService.Identity
{
    public interface IUniquePhoneNumberUserLookupAppService : IApplicationService
    {
        Task<IUserData> FindByConfirmedPhoneNumberAsync(string phoneNumber);
    }
}