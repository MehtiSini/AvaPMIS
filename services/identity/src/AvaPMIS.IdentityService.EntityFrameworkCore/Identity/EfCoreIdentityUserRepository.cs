using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AvaPMIS.IdentityService.Localization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace AvaPMIS.IdentityService.Identity
{
    [Dependency(ReplaceServices = true)]
    public class EfCoreIdentityUserRepository : Volo.Abp.Identity.EntityFrameworkCore.EfCoreIdentityUserRepository, IIdentityUserRepository,ITransientDependency
    {
        


        public EfCoreIdentityUserRepository(
            IDbContextProvider<IIdentityDbContext> dbContextProvider) : base(dbContextProvider)
        {
          
        }
        public virtual async Task<IdentityUser> FindByConfirmedPhoneNumberAsync(string phoneNumber, bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            return includeDetails
                ? await (await WithDetailsAsync()).FirstOrDefaultAsync(e => e.PhoneNumber == phoneNumber,
                    GetCancellationToken(cancellationToken))
                : await (await GetDbSetAsync()).FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber,
                    GetCancellationToken(cancellationToken));
        }

        public virtual async Task<IdentityUser> GetByConfirmedPhoneNumberAsync(string phoneNumber, bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            var identityUser =
                await FindByConfirmedPhoneNumberAsync(phoneNumber, includeDetails, GetCancellationToken(cancellationToken));

            if (identityUser == null)
            {
                throw new EntityNotFoundException(typeof(IdentityUser));
            }

            return identityUser;
        }

        public virtual async Task<bool> IsPhoneNumberUedAsync(
            string phoneNumber,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync()).IncludeDetails(false)
                .AnyAsync(user => user.PhoneNumber == phoneNumber,
                    GetCancellationToken(cancellationToken));
        }

        public virtual async Task<bool> IsPhoneNumberConfirmedAsync(
            string phoneNumber,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync()).IncludeDetails(false)
                .AnyAsync(user => user.PhoneNumber == phoneNumber && user.PhoneNumberConfirmed,
                    GetCancellationToken(cancellationToken));
        }

        public virtual async Task<bool> IsNormalizedEmailConfirmedAsync(
           string normalizedEmail,
           CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync()).IncludeDetails(false)
                .AnyAsync(user => user.NormalizedEmail == normalizedEmail && user.EmailConfirmed,
                    GetCancellationToken(cancellationToken));
        }

        public virtual async Task<IdentityUser> FindByPhoneNumberAsync(
            string phoneNumber,
            bool? isConfirmed = true,
            bool includeDetails = false,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync()).IncludeDetails(includeDetails)
               .Where(user => user.PhoneNumber == phoneNumber &&(!isConfirmed.HasValue || user.PhoneNumberConfirmed == isConfirmed))
               .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        }
    
        public virtual async Task<List<IdentityUser>> GetListByIdListAsync(
            List<Guid> userIds,
            bool includeDetails = false,
            CancellationToken cancellationToken = default
            )
        {
            return await (await GetDbSetAsync()).IncludeDetails(includeDetails)
                .Where(user => userIds.Contains(user.Id))
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<OrganizationUnit>> GetOrganizationUnitsAsync(
            Guid id,
            string filter = null,
            bool includeDetails = false,
            int skipCount = 1,
            int maxResultCount = 10,
            CancellationToken cancellationToken = default
        )
        {
            var dbContext = await GetDbContextAsync();
            var query = from userOU in dbContext.Set<IdentityUserOrganizationUnit>()
                        join ou in dbContext.OrganizationUnits.IncludeDetails(includeDetails) on userOU.OrganizationUnitId equals ou.Id
                        where userOU.UserId == id
                        select ou;

            return await query
                .WhereIf(!filter.IsNullOrWhiteSpace(), ou => ou.Code.Contains(filter) || ou.DisplayName.Contains(filter))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> GetUsersInOrganizationUnitCountAsync(
            Guid organizationUnitId,
            string filter = null,
            CancellationToken cancellationToken = default
        )
        {
            var dbContext = await GetDbContextAsync();
            var query = from userOu in dbContext.Set<IdentityUserOrganizationUnit>()
                        join user in (await GetDbSetAsync()) on userOu.UserId equals user.Id
                        where userOu.OrganizationUnitId == organizationUnitId
                        select user;
            return await query
                .WhereIf(!filter.IsNullOrWhiteSpace(), 
                    user => user.Name.Contains(filter) || user.UserName.Contains(filter) ||
                        user.Surname.Contains(filter) || user.Email.Contains(filter) ||
                        user.PhoneNumber.Contains(filter))
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<IdentityUser>> GetUsersInOrganizationUnitAsync(
            Guid organizationUnitId,
            string filter = null,
            int skipCount = 1,
            int maxResultCount = 10,
            CancellationToken cancellationToken = default
        )
        {
            var dbContext = await GetDbContextAsync();
            var query = from userOu in dbContext.Set<IdentityUserOrganizationUnit>()
                        join user in (await GetDbSetAsync()) on userOu.UserId equals user.Id
                        where userOu.OrganizationUnitId == organizationUnitId
                        select user;
            return await query
                .WhereIf(!filter.IsNullOrWhiteSpace(),
                    user => user.Name.Contains(filter) || user.UserName.Contains(filter) ||
                        user.Surname.Contains(filter) || user.Email.Contains(filter) ||
                        user.PhoneNumber.Contains(filter))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> GetUsersInOrganizationsListCountAsync(
            List<Guid> organizationUnitIds,
            string filter = null,
            CancellationToken cancellationToken = default
        )
        {
            var dbContext = await GetDbContextAsync();
            var query = from userOu in dbContext.Set<IdentityUserOrganizationUnit>()
                        join user in (await GetDbSetAsync()) on userOu.UserId equals user.Id
                        where organizationUnitIds.Contains(userOu.OrganizationUnitId)
                        select user;
            return await query
                .WhereIf(!filter.IsNullOrWhiteSpace(),
                    user => user.Name.Contains(filter) || user.UserName.Contains(filter) ||
                        user.Surname.Contains(filter) || user.Email.Contains(filter) ||
                        user.PhoneNumber.Contains(filter))
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<IdentityUser>> GetUsersInOrganizationsListAsync(
            List<Guid> organizationUnitIds,
            string filter = null,
            int skipCount = 1,
            int maxResultCount = 10,
            CancellationToken cancellationToken = default
        )
        {
            var dbContext = await GetDbContextAsync();
            var query = from userOu in dbContext.Set<IdentityUserOrganizationUnit>()
                        join user in (await GetDbSetAsync()) on userOu.UserId equals user.Id
                        where organizationUnitIds.Contains(userOu.OrganizationUnitId)
                        select user;
            return await query
                .WhereIf(!filter.IsNullOrWhiteSpace(),
                    user => user.Name.Contains(filter) || user.UserName.Contains(filter) ||
                        user.Surname.Contains(filter) || user.Email.Contains(filter) ||
                        user.PhoneNumber.Contains(filter))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> GetUsersInOrganizationUnitWithChildrenCountAsync(
            string code,
            string filter = null,
            CancellationToken cancellationToken = default
        )
        {
            var dbContext = await GetDbContextAsync();
            var query = from userOu in dbContext.Set<IdentityUserOrganizationUnit>()
                        join user in (await GetDbSetAsync()) on userOu.UserId equals user.Id
                        join ou in dbContext.Set<OrganizationUnit>() on userOu.OrganizationUnitId equals ou.Id
                        where ou.Code.StartsWith(code)
                        select user;
            return await query
                .WhereIf(!filter.IsNullOrWhiteSpace(),
                    user => user.Name.Contains(filter) || user.UserName.Contains(filter) ||
                        user.Surname.Contains(filter) || user.Email.Contains(filter) ||
                        user.PhoneNumber.Contains(filter))
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<IdentityUser>> GetUsersInOrganizationUnitWithChildrenAsync(
            string code,
            string filter = null,
            int skipCount = 1,
            int maxResultCount = 10,
            CancellationToken cancellationToken = default
        )
        {
            var dbContext = await GetDbContextAsync();
            var query = from userOu in dbContext.Set<IdentityUserOrganizationUnit>()
                        join user in (await GetDbSetAsync()) on userOu.UserId equals user.Id
                        join ou in dbContext.Set<OrganizationUnit>() on userOu.OrganizationUnitId equals ou.Id
                        where ou.Code.StartsWith(code)
                        select user;
            return await query
                .WhereIf(!filter.IsNullOrWhiteSpace(),
                    user => user.Name.Contains(filter) || user.UserName.Contains(filter) ||
                        user.Surname.Contains(filter) || user.Email.Contains(filter) ||
                        user.PhoneNumber.Contains(filter))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public string GenerateUserName(string phoneNumber)
        {
            return phoneNumber;
        }
        public string GenerateEmail(string phoneNumber)
        {
            var emailTemplate = "{0}@ajp-co.com";
            return string.Format(emailTemplate,phoneNumber);

        }

        private string GeneratePassword(PasswordOptions options)
        {
            if (options == null)
                return null;

            bool requireNonLetterOrDigit = options.RequireNonAlphanumeric;
            bool requireDigit = options.RequireDigit;
            bool requireLowercase = options.RequireLowercase;
            bool requireUppercase = options.RequireUppercase;

            string randomPassword = string.Empty;

            int passwordLength = options.RequiredLength;

            Random random = new Random();
            while (randomPassword.Length != passwordLength)
            {
                int randomNumber = random.Next(48, 122);  // >= 48 && < 122 
                if (randomNumber == 95 || randomNumber == 96) continue;  // != 95, 96 _'

                char c = Convert.ToChar(randomNumber);

                if (requireDigit)
                    if (char.IsDigit(c))
                        requireDigit = false;

                if (requireLowercase)
                    if (char.IsLower(c))
                        requireLowercase = false;

                if (requireUppercase)
                    if (char.IsUpper(c))
                        requireUppercase = false;

                if (requireNonLetterOrDigit)
                    if (!char.IsLetterOrDigit(c))
                        requireNonLetterOrDigit = false;

                randomPassword += c;
            }

            if (requireDigit)
                randomPassword += Convert.ToChar(random.Next(48, 58));  // 0-9

            if (requireLowercase)
                randomPassword += Convert.ToChar(random.Next(97, 123));  // a-z

            if (requireUppercase)
                randomPassword += Convert.ToChar(random.Next(65, 91));  // A-Z

            if (requireNonLetterOrDigit)
                randomPassword += Convert.ToChar(random.Next(33, 48));  // symbols !"#$%&'()*+,-./

            return randomPassword;
        }
    }
}
