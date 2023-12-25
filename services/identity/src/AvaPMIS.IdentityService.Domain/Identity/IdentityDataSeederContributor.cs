using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;

namespace AvaPMIS.IdentityService.Identity
{
    public class IdentityDataSeederContributor
        : IDataSeedContributor, ITransientDependency
    {
        private readonly IdentityUserManager _identityUserManager;
        private readonly IdentityRoleManager _identityRoleManager;
        public IdentityDataSeederContributor(IdentityUserManager identityUserManager, IdentityRoleManager identityRoleManager)
        {
            _identityUserManager = identityUserManager;
            _identityRoleManager = identityRoleManager;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            // Add users
            IdentityUser identityUser1 = new IdentityUser(Guid.NewGuid(), "testAdmin1", "testuser1@email.com");
            if ((await _identityRoleManager.FindByNameAsync(IdentityPermissionsConsts.Roles.Admin)) == null)
            {
                var role = new IdentityRole(Guid.NewGuid(), IdentityPermissionsConsts.Roles.Admin);
                await _identityRoleManager.CreateAsync(role);
            }

            if ((await _identityRoleManager.FindByNameAsync(IdentityPermissionsConsts.Roles.Operator)) == null)
            {
                var role = new IdentityRole(Guid.NewGuid(), IdentityPermissionsConsts.Roles.Operator);
                await _identityRoleManager.CreateAsync(role);
            }
            if ((await _identityUserManager.FindByNameAsync("testAdmin1")) == null)
            {
                await _identityUserManager.CreateAsync(identityUser1, "1q2w3E*");
                await _identityUserManager.AddToRoleAsync(identityUser1, IdentityPermissionsConsts.Roles.Admin);
                identityUser1.SetPhoneNumber("09012221808", true);
                await _identityUserManager.UpdateAsync(identityUser1);
            }
          
        }
    }
}