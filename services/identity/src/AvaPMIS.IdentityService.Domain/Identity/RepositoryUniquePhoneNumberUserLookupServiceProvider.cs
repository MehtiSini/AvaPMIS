﻿using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Users;

namespace AvaPMIS.IdentityService.Identity
{
    public class RepositoryUniquePhoneNumberUserLookupServiceProvider : IUniquePhoneNumberUserLookupServiceProvider, ITransientDependency
    {
        private readonly IIdentityUserRepository _userRepository;

        public RepositoryUniquePhoneNumberUserLookupServiceProvider(
            IIdentityUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public virtual async Task<IUserData> FindByConfirmedPhoneNumberAsync(string phoneNumber,
            CancellationToken cancellationToken = default)
        {
            return (await _userRepository.FindByConfirmedPhoneNumberAsync(phoneNumber, false, cancellationToken))
                ?.ToAbpUserData();
        }
    }
}