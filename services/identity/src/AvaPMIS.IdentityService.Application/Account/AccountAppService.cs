﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using AvaPMIS.IdentityService.Account.Dtos;
using AvaPMIS.IdentityService.Identity;
using AvaPMIS.IdentityService.Localization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Nozhan.Abp.Utilities.Identity;
using Nozhan.Abp.Utilities.Identity.Security;
using Volo.Abp;
using Volo.Abp.Account.Emailing;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Settings;
using Volo.Abp.Settings;
using Volo.Abp.Validation;
using IIdentityRoleRepository = AvaPMIS.IdentityService.Identity.IIdentityRoleRepository;
using IIdentityUserRepository = AvaPMIS.IdentityService.Identity.IIdentityUserRepository;


namespace AvaPMIS.IdentityService.Account
{
    [Dependency(ReplaceServices = true)]
    //[Route("api/account/phone")]
    public class PhoneAccountAppService : ApplicationService, IPhoneAccountAppService, IScopedDependency
    {
        protected ITotpService TotpService { get; }
        protected IdentityUserStore UserStore { get; }
        protected IdentityUserManager UserManager { get; }
        protected IIdentityUserRepository UserRepository { get; }
        protected IUserSecurityCodeSender SecurityCodeSender { get; }
        protected IOptions<IdentityOptions> IdentityOptions { get; }
        protected IDistributedCache<SmsSecurityTokenCacheItem> SecurityTokenCache { get; }

        public PhoneAccountAppService(
            ITotpService totpService,
            IdentityUserStore userStore,
            IdentityUserManager userManager,
            IIdentityUserRepository userRepository,
            IIdentityRoleRepository roleRepository,
            IUserSecurityCodeSender securityCodeSender,
            IAccountEmailer accountEmailer,
            IdentitySecurityLogManager logManager,
            IDistributedCache<SmsSecurityTokenCacheItem> securityTokenCache,
            IOptions<IdentityOptions> identityOptions) 
        {
            TotpService = totpService;
            UserStore = userStore;
            UserManager = userManager;
            UserRepository = userRepository;
            SecurityCodeSender = securityCodeSender;
            SecurityTokenCache = securityTokenCache;
            IdentityOptions = identityOptions;

            LocalizationResource = typeof(IdentityServiceResource);
        }

        [HttpPost]
        [Route("api/account/phone/send-register-code")]
        public async virtual Task SendPhoneRegisterCodeAsync(SendPhoneRegisterCodeDto input)
        {
            await CheckSelfRegistrationAsync();
            await CheckNewUserPhoneNumberNotBeUsedAsync(input.PhoneNumber);

            var securityTokenCacheKey = SmsSecurityTokenCacheItem.CalculateCacheKey(input.PhoneNumber, "SmsVerifyCode");
            var securityTokenCacheItem = await SecurityTokenCache.GetAsync(securityTokenCacheKey);
            var interval = await SettingProvider.GetAsync(AvaPMIS.IdentityService.Identity.Settings.IdentitySettingNames.User.SmsRepetInterval, 1);

            if (securityTokenCacheItem != null)
            {
                throw new UserFriendlyException(L["SendRepeatSmsVerifyCode", interval]);
            }

            var template = await SettingProvider.GetOrNullAsync(AvaPMIS.IdentityService.Identity.Settings.IdentitySettingNames.User.SmsNewUserRegister);

            // 安全令牌
            var securityToken = GuidGenerator.Create().ToString("N");

            var code = TotpService.GenerateCode(Encoding.Unicode.GetBytes(securityToken), securityTokenCacheKey);
            securityTokenCacheItem = new SmsSecurityTokenCacheItem(code.ToString(), securityToken);

            await SecurityCodeSender.SendPhoneConfirmedCodeAsync(
                input.PhoneNumber, securityTokenCacheItem.Token, template);

            await SecurityTokenCache
                .SetAsync(securityTokenCacheKey, securityTokenCacheItem,
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(interval)
                    });
        }
        //[HttpPost]
        [Route("api/account/phone/register")]
        public async virtual Task RegisterAsync(PhoneRegisterDto input)
        {
            await CheckSelfRegistrationAsync();
            await IdentityOptions.SetAsync();
            await CheckNewUserPhoneNumberNotBeUsedAsync(input.PhoneNumber);

            var securityTokenCacheKey = SmsSecurityTokenCacheItem.CalculateCacheKey(input.PhoneNumber, "SmsVerifyCode");
            var securityTokenCacheItem = await SecurityTokenCache.GetAsync(securityTokenCacheKey);
            if (securityTokenCacheItem == null)
            {
                // 验证码过期
                throw new UserFriendlyException(L["InvalidSmsVerifyCode"]);
            }

            // 验证码是否有效
            if (input.Code.Equals(securityTokenCacheItem.Token) && int.TryParse(input.Code, out int token))
            {
                var securityToken = Encoding.Unicode.GetBytes(securityTokenCacheItem.SecurityToken);
                // 校验totp验证码
                if (TotpService.ValidateCode(securityToken, token, securityTokenCacheKey))
                {
                    var userEmail = input.EmailAddress ?? $"{input.PhoneNumber}@{CurrentTenant.Name ?? "default"}.io";//如果邮件地址不验证,随意写入一个
                    var userName = input.UserName ?? input.PhoneNumber;
                    var user = new Volo.Abp.Identity.IdentityUser(GuidGenerator.Create(), userName, userEmail, CurrentTenant.Id)
                    {
                        Name = input.Name ?? input.PhoneNumber
                    };

                    await UserStore.SetPhoneNumberAsync(user, input.PhoneNumber);
                    await UserStore.SetPhoneNumberConfirmedAsync(user, true);

                    (await UserManager.CreateAsync(user, input.Password)).CheckErrors();

                    (await UserManager.AddDefaultRolesAsync(user)).CheckErrors();

                    await SecurityTokenCache.RemoveAsync(securityTokenCacheKey);

                    await CurrentUnitOfWork.SaveChangesAsync();

                    return;
                }
            }
            // 验证码无效
            throw new UserFriendlyException(L["InvalidSmsVerifyCode"]);
        }

        [HttpPost]
        [Route("api/account/phone/send-password-reset-code")]
        public async virtual Task SendPhoneResetPasswordCodeAsync(SendPhoneResetPasswordCodeDto input)
        {
            /*
             * 注解: 微软的重置密码方法通过 UserManager.GeneratePasswordResetTokenAsync 接口生成密码重置Token
             *       而这个Token设计的意义就是用户通过链接来重置密码,所以不适合短信验证
             *       某些企业是把链接生成一个短链发送短信的,不过这种方式不是很推荐,因为现在是真没几个人敢随便点短信链接的
             *  
             *  此处设计方式为:
             *  
             *  step1: 例行检查是否重复发送,这一点是很有必要的
             *  step2: 通过已确认的手机号来查询用户,如果用户未确认手机号,那就不能发送,这一点也是很有必要的
             *  step3(重点): 通过 UserManager.GenerateTwoFactorTokenAsync 接口来生成二次认证码,这就相当于伪验证码,只是用于确认用户传递的验证码是否通过
             *               比起自己生成随机数,这个验证码利用了TOTP算法,有时间限制的
             *  step4(重点): 用户传递验证码后,通过 UserManager.VerifyTwoFactorTokenAsync 接口来校验验证码
             *               验证通过后,再利用 UserManager.GeneratePasswordResetTokenAsync 接口来生成真正的用于重置密码的Token
            */

            var securityTokenCacheKey = SmsSecurityTokenCacheItem.CalculateCacheKey(input.PhoneNumber, "SmsVerifyCode");
            var securityTokenCacheItem = await SecurityTokenCache.GetAsync(securityTokenCacheKey);
            var interval = await SettingProvider.GetAsync(AvaPMIS.IdentityService.Identity.Settings.IdentitySettingNames.User.SmsRepetInterval, 1);
            // 传递 isConfirmed 用户必须是已确认过手机号的
            var user = await GetUserByPhoneNumberAsync(input.PhoneNumber, isConfirmed: true);
            // 能查询到缓存就是重复发送
            if (securityTokenCacheItem != null)
            {
                throw new UserFriendlyException(L["SendRepeatSmsVerifyCode", interval]);
            }

            var template = await SettingProvider.GetOrNullAsync(AvaPMIS.IdentityService.Identity.Settings.IdentitySettingNames.User.SmsResetPassword);
            // 生成二次认证码
            var code = await UserManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider);
            // 发送短信验证码
            await SecurityCodeSender.SendPhoneConfirmedCodeAsync(input.PhoneNumber, code, template);
            // 缓存这个手机号的记录,防重复
            securityTokenCacheItem = new SmsSecurityTokenCacheItem(code, user.SecurityStamp);
            await SecurityTokenCache
                .SetAsync(securityTokenCacheKey, securityTokenCacheItem,
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(interval)
                    });
        }
        [HttpPut]
        [Route("api/account/phone/reset-password")]
        public async virtual Task ResetPasswordAsync(PhoneResetPasswordDto input)
        {
            var securityTokenCacheKey = SmsSecurityTokenCacheItem.CalculateCacheKey(input.PhoneNumber, "SmsVerifyCode");
            var securityTokenCacheItem = await SecurityTokenCache.GetAsync(securityTokenCacheKey);
            if (securityTokenCacheItem == null)
            {
                throw new UserFriendlyException(L["InvalidSmsVerifyCode"]);
            }
            await IdentityOptions.SetAsync();
            // 传递 isConfirmed 用户必须是已确认过手机号的
            var user = await GetUserByPhoneNumberAsync(input.PhoneNumber, isConfirmed: true);
            // 验证二次认证码
            if (!await UserManager.VerifyTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider, input.Code))
            {
                // 验证码无效
                throw new UserFriendlyException(L["InvalidSmsVerifyCode"]);
            }
            // 生成真正的重置密码Token
            var resetPwdToken = await UserManager.GeneratePasswordResetTokenAsync(user);
            // 重置密码
            (await UserManager.ResetPasswordAsync(user, resetPwdToken, input.NewPassword)).CheckErrors();
            // 移除缓存项
            await SecurityTokenCache.RemoveAsync(securityTokenCacheKey);

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [HttpPost]
        [Route("api/account/phone/send-signin-code")]
        public async virtual Task SendPhoneSigninCodeAsync(SendPhoneSigninCodeDto input)
        {
            var securityTokenCacheKey = SmsSecurityTokenCacheItem.CalculateCacheKey(input.PhoneNumber, "SmsVerifyCode");
            var securityTokenCacheItem = await SecurityTokenCache.GetAsync(securityTokenCacheKey);
            var interval = await SettingProvider.GetAsync(AvaPMIS.IdentityService.Identity.Settings.IdentitySettingNames.User.SmsRepetInterval, 1);
            if (securityTokenCacheItem != null)
            {
                throw new UserFriendlyException(L["SendRepeatSmsVerifyCode", interval]);
            }
            // 传递 isConfirmed 验证过的用户才允许通过手机登录
            var user = await GetUserByPhoneNumberAsync(input.PhoneNumber, isConfirmed: true);
            var code = await UserManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider);
            var template = await SettingProvider.GetOrNullAsync(AvaPMIS.IdentityService.Identity.Settings.IdentitySettingNames.User.SmsUserSignin);

            // 发送登录验证码短信
            await SecurityCodeSender.SendPhoneConfirmedCodeAsync(input.PhoneNumber, code, template);
            // 缓存登录验证码状态,防止同一手机号重复发送
            securityTokenCacheItem = new SmsSecurityTokenCacheItem(code, user.SecurityStamp);
            await SecurityTokenCache
                .SetAsync(securityTokenCacheKey, securityTokenCacheItem,
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(interval)
                    });
        }

        protected async virtual Task<Volo.Abp.Identity.IdentityUser> GetUserByPhoneNumberAsync(string phoneNumber, bool isConfirmed = true)
        {
            var user = await UserRepository.FindByPhoneNumberAsync(phoneNumber, isConfirmed, true);
            if (user == null)
            {
                throw new UserFriendlyException(L["PhoneNumberNotRegisterd"]);
            }
            return user;
        }

        /// <summary>
        /// 检查是否允许用户注册
        /// </summary>
        /// <returns></returns>
        protected async virtual Task CheckSelfRegistrationAsync()
        {
            if (!await SettingProvider.IsTrueAsync(Volo.Abp.Account.Settings.AccountSettingNames.IsSelfRegistrationEnabled))
            {
                throw new UserFriendlyException(L["SelfRegistrationDisabledMessage"]);
            }
        }

        protected async virtual Task CheckNewUserPhoneNumberNotBeUsedAsync(string phoneNumber)
        {
            if (await UserRepository.IsPhoneNumberUedAsync(phoneNumber))
            {
                throw new UserFriendlyException(L["DuplicatePhoneNumber"]);
            }
        }

        private void ThowIfInvalidEmailAddress(string inputEmail)
        {
            if (!inputEmail.IsNullOrWhiteSpace() &&
                !ValidationHelper.IsValidEmailAddress(inputEmail))
            {
                throw new AbpValidationException(
                    new ValidationResult[]
                    {
                        new ValidationResult(L["The {0} field is not a valid e-mail address.", L["DisplayName:EmailAddress"]], new string[]{ "EmailAddress" })
                    });
            }
        }
    }
}
