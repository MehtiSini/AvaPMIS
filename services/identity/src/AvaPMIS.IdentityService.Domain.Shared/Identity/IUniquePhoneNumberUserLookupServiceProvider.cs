using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Users;

namespace AvaPMIS.IdentityService.Identity
{

    public interface IUniquePhoneNumberUserLookupServiceProvider
    {
        Task<IUserData> FindByConfirmedPhoneNumberAsync([NotNull] string phoneNumber, CancellationToken cancellationToken = default);
    }
}
