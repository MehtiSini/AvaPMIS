using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Identity;

namespace AvaPMIS.IdentityService.Identity
{
    public interface IIdentityUserRepository : Volo.Abp.Identity.IIdentityUserRepository
    {
        Task<IdentityUser> FindByConfirmedPhoneNumberAsync([NotNull] string phoneNumber, bool includeDetails = true, CancellationToken cancellationToken = default);

        Task<IdentityUser> GetByConfirmedPhoneNumberAsync([NotNull] string phoneNumber, bool includeDetails = true, CancellationToken cancellationToken = default);
        Task<bool> IsPhoneNumberUedAsync(
            string phoneNumber,
            CancellationToken cancellationToken = default);
        Task<bool> IsPhoneNumberConfirmedAsync(
            string phoneNumber,
            CancellationToken cancellationToken = default);
        Task<bool> IsNormalizedEmailConfirmedAsync(
           string normalizedEmail,
           CancellationToken cancellationToken = default);

        Task<IdentityUser> FindByPhoneNumberAsync(
            string phoneNumber,
            bool? isConfirmed = true,
            bool includeDetails = false,
            CancellationToken cancellationToken = default);
        Task<List<IdentityUser>> GetListByIdListAsync(
            List<Guid> userIds,
            bool includeDetails = false,
            CancellationToken cancellationToken = default
            );
        Task<List<OrganizationUnit>> GetOrganizationUnitsAsync(
            Guid userId, 
            string filter = null,
            bool includeDetails = false, 
            int skipCount = 1, 
            int maxResultCount = 10, 
            CancellationToken cancellationToken = default
        );
        Task<long> GetUsersInOrganizationUnitCountAsync(
            Guid organizationUnitId,
            string filter = null,
            CancellationToken cancellationToken = default
        );

        Task<List<IdentityUser>> GetUsersInOrganizationUnitAsync(
            Guid organizationUnitId,
            string filter = null,
            int skipCount = 1,
            int maxResultCount = 10,
            CancellationToken cancellationToken = default
        );

        Task<long> GetUsersInOrganizationsListCountAsync(
            List<Guid> organizationUnitIds,
            string filter = null,
            CancellationToken cancellationToken = default
        );

        Task<List<IdentityUser>> GetUsersInOrganizationsListAsync(
            List<Guid> organizationUnitIds,
            string filter = null,
            int skipCount = 1,
            int maxResultCount = 10,
            CancellationToken cancellationToken = default
        );

        Task<long> GetUsersInOrganizationUnitWithChildrenCountAsync(
            string code,
            string filter = null,
            CancellationToken cancellationToken = default
        );

        Task<List<IdentityUser>> GetUsersInOrganizationUnitWithChildrenAsync(
            string code,
            string filter = null,
            int skipCount = 1,
            int maxResultCount = 10,
            CancellationToken cancellationToken = default
        );
    }
}
