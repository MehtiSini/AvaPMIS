using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace AvaPMIS.IdentityService.Identity
{
    public interface IPhoneNumberLoginOrNewUserCreator
    {

        public class FindOrConfirmOrCreateResult
        {
            public IdentityUser User { get; set; }
            public FindOrConfirmOrCreateStatus Status { get; set; } = FindOrConfirmOrCreateStatus.None;
        }
        Task<IdentityUser> CreateAsync(string phoneNumber, string userName = null, string password = null, string email = null, bool confirmPhoneNumber=false);

        Task<FindOrConfirmOrCreateResult> FindOrCreateAsync(string phoneNumber);
    }
}