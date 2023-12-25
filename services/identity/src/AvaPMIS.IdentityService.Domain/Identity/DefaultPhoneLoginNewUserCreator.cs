using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using static AvaPMIS.IdentityService.Identity.IPhoneNumberLoginOrNewUserCreator;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace AvaPMIS.IdentityService.Identity
{
    [Dependency(TryRegister = true)]
    public class DefaultPhoneLoginOrNewUserCreator : IPhoneNumberLoginOrNewUserCreator, ITransientDependency
    {
        
        private readonly ICurrentTenant _currentTenant;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IOptions<IdentityOptions> _identityOptions;
        private readonly IdentityUserManager _identityUserManager;
        private readonly IIdentityUserRepository _identityUserRepository;


        public DefaultPhoneLoginOrNewUserCreator(
            ICurrentTenant currentTenant,
            IGuidGenerator guidGenerator,
            IOptions<IdentityOptions> identityOptions,
            IdentityUserManager identityUserManager,
            IIdentityUserRepository identityUserRepository)
        {
            _currentTenant = currentTenant;
            _guidGenerator = guidGenerator;
            _identityOptions = identityOptions;
            _identityUserManager = identityUserManager;
            _identityUserRepository = identityUserRepository;
        }

        public virtual async Task<Volo.Abp.Identity.IdentityUser> CreateAsync(string phoneNumber,
            string userName = null, string password = null, string email = null,bool confirmPhoneNumber=false)
        {
            await _identityOptions.SetAsync();
            IdentityUser identityUser = new IdentityUser(Guid.NewGuid(), 
                userName ?? await GenerateUserNameAsync(phoneNumber), 
                email ?? await GenerateEmailAsync(phoneNumber));
            
            if (!string.IsNullOrEmpty(password))
                await _identityUserManager.CreateAsync(identityUser, password);
            else
            {
                await _identityUserManager.CreateAsync(identityUser);
            }
            identityUser.SetPhoneNumber(phoneNumber, confirmPhoneNumber);
            await _identityUserManager.UpdateAsync(identityUser);
            //identityUser.Name = userName ?? phoneNumber;

            //(await _identityUserManager.UpdateAsync(identityUser)).CheckErrors();

            (await _identityUserManager.AddDefaultRolesAsync(identityUser)).CheckErrors();

            return identityUser;
        }

        public virtual async Task<FindOrConfirmOrCreateResult> FindOrCreateAsync(
            string phoneNumber)
        {
            await _identityOptions.SetAsync();
            FindOrConfirmOrCreateResult result = new FindOrConfirmOrCreateResult();
            IdentityUser user = null;
            user = await _identityUserRepository.FindByPhoneNumberAsync(phoneNumber, null, false);
            if (user == null)
            {
                var email = await GenerateEmailAsync(phoneNumber);
                var userName = await GenerateUserNameAsync(phoneNumber);
                string password = null; //await GeneratePasswordAsync(_identityOptions.Value.Password);
                user = await CreateAsync(phoneNumber, userName, password, email);
                result.Status = FindOrConfirmOrCreateStatus.NotExistedAndCreated;
            }
            else
            {
                if (user.PhoneNumberConfirmed == false)
                {
                    result.Status = FindOrConfirmOrCreateStatus.ExistedButNutConfirmed;

                }
                else
                {
                    result.Status = FindOrConfirmOrCreateStatus.ExistedAndConfimed;
                }
            }

            result.User = user;
            return result;
        }

        public async Task<string> GenerateUserNameAsync(string phoneNumber)
        {
            var usernameTemplate = "{0}";
            return string.Format(usernameTemplate,phoneNumber);
        }

        public async Task<string> GenerateEmailAsync(string phoneNumber)
        {
            var emailTemplate = "{0}@ajp-co.com";
            return string.Format(emailTemplate, phoneNumber);

        }

        private async Task<string> GeneratePasswordAsync(PasswordOptions options)
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
                int randomNumber = random.Next(48, 122); // >= 48 && < 122 
                if (randomNumber == 95 || randomNumber == 96) continue; // != 95, 96 _'

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
                randomPassword += Convert.ToChar(random.Next(48, 58)); // 0-9

            if (requireLowercase)
                randomPassword += Convert.ToChar(random.Next(97, 123)); // a-z

            if (requireUppercase)
                randomPassword += Convert.ToChar(random.Next(65, 91)); // A-Z

            if (requireNonLetterOrDigit)
                randomPassword += Convert.ToChar(random.Next(33, 48)); // symbols !"#$%&'()*+,-./

            return randomPassword;
        }
    }
}