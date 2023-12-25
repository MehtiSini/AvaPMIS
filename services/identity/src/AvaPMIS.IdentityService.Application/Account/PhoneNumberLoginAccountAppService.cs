using Microsoft.AspNetCore.Mvc;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;
using AvaPMIS.IdentityService.Account.Dtos;
using AvaPMIS.IdentityService.Identity;
using AvaPMIS.IdentityService.Settings;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Nozhan.Abp.Utilities.Identity;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Settings;
using Volo.Abp.Settings;
using Volo.Abp.Uow;
using Volo.Abp.Validation;
using IdentityUser = Volo.Abp.Identity.IdentityUser;
using IIdentityUserRepository = AvaPMIS.IdentityService.Identity.IIdentityUserRepository;
using VerificationCodeType = Nozhan.Abp.Utilities.Identity.VerificationCodeType;
using AvaPMIS.IdentityService.Cookie;
using Microsoft.AspNetCore.Http;

namespace AvaPMIS.IdentityService.Account
{

    public class PhoneNumberLoginAccountAppService : ApplicationService, IPhoneNumberLoginAccountAppService
    {
        IUserSecurityCodeSender _securityCodeSender;
        private readonly IDistributedCache<SmsSecurityTokenCacheItem> _securityTokenCache;
        private readonly IPhoneNumberLoginOrNewUserCreator _phoneNumberLoginOrNewUserCreator;
        //private readonly IVerificationCodeManager _verificationCodeManager;
        private readonly IOptions<IdentityOptions> _identityOptions;
        private readonly IdentityUserManager _identityUserManager;
        private readonly IIdentityUserRepository _userRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ISettingProvider _settingProvider;
        private readonly ILogger<PhoneNumberLoginAccountAppService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CookieHelper _cookieHelper;
        public PhoneNumberLoginAccountAppService(
            IUserSecurityCodeSender securityCodeSender,
            IDistributedCache<SmsSecurityTokenCacheItem> securityTokenCache,
            IPhoneNumberLoginOrNewUserCreator phoneNumberLoginOrNewUserCreator,
            IIdentityUserRepository userRepository,
            //IVerificationCodeManager verificationCodeManager,

            IOptions<IdentityOptions> identityOptions,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            ISettingProvider settingProvider,
            IdentityUserManager identityUserManager, ILogger<PhoneNumberLoginAccountAppService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _securityCodeSender = securityCodeSender;
            _securityTokenCache = securityTokenCache;
            _phoneNumberLoginOrNewUserCreator = phoneNumberLoginOrNewUserCreator;
            //_verificationCodeManager = verificationCodeManager;
            _userRepository = userRepository;
            _identityOptions = identityOptions;
            _identityUserManager = identityUserManager;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _settingProvider = settingProvider;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _cookieHelper = new CookieHelper(_httpContextAccessor);
        }

        [Route("api/account/phone/sendSignInCode")]
        public async virtual Task<SendSignInCodeResult> SendSignInCodeAsync(SendPhoneOtpCodeDto input)
        {
            var res = new SendSignInCodeResult(SendSignInCodeResultType.SendsFailure);
            var securityTokenCacheKey = SmsSecurityTokenCacheItem.CalculateCacheKey(input.PhoneNumber, "SmsVerifyCode");
            var securityTokenCacheItem = await _securityTokenCache.GetAsync(securityTokenCacheKey);
            var interval = await SettingProvider.GetAsync(AvaPMIS.IdentityService.Settings.IdentityServiceSettingNames.SmsRepetInterval, 2);
            if (securityTokenCacheItem != null)
            {
                res = new SendSignInCodeResult(SendSignInCodeResultType.FrequencyIsLimited);
                return res;
            }

            var userRes = await FindOrCreateAsync(input.PhoneNumber);

            var user = userRes.User;
            //var code = await _identityUserManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider);
            var code = await _identityUserManager.GenerateUserTokenAsync(user, TokenOptions.DefaultPhoneProvider,
                PhoneNumberLoginConsts.LoginPurposeName);
            var template = await SettingProvider.GetOrNullAsync(IdentityServiceSettingNames.SmsUserSignin);

            var sendingResult = await _securityCodeSender.SendAsync(VerificationSendingMethod.SMS, input.PhoneNumber, template, code, VerificationCodeType.SignInCode);
            res = new SendSignInCodeResult(SendSignInCodeResultType.Success, userRes.Status);

            securityTokenCacheItem = new SmsSecurityTokenCacheItem(code, user.SecurityStamp);
            await _securityTokenCache
                .SetAsync(securityTokenCacheKey, securityTokenCacheItem,
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(interval)
                    });
            return res;
        }
        protected async virtual Task<IPhoneNumberLoginOrNewUserCreator.FindOrConfirmOrCreateResult> FindOrCreateAsync(string phoneNumber)
        {
            var result = await _phoneNumberLoginOrNewUserCreator.FindOrCreateAsync(phoneNumber);
            if (result == null || result.Status == FindOrConfirmOrCreateStatus.None)
            {
                throw new UserFriendlyException(L["PhoneNumberNotRegisterd"]);
            }
            return result;
        }
        //[RemoteService(IsEnabled = false)]
        //public virtual async Task<SendVerificationCodeResult> SendVerificationCodeAsync(SendVerificationCodeInput input)
        //{
        //    var identityUser = await _userRepository.FindByConfirmedPhoneNumberAsync(input.PhoneNumber);

        //    string code = await GenerateCodeAsync(input.PhoneNumber, input.VerificationCodeType, identityUser);

        //    var result = await _securityCodeSender.SendAsync(VerificationSendingMethod.SMS,input.PhoneNumber, "{0}",code,
        //        input.VerificationCodeType,
        //        new { LifespanMinutes = Math.Floor(await GetRegisterCodeCacheSecondsAsync() / 60f) });

        //    return result ? new SendVerificationCodeResult(SendVerificationCodeResultType.Success) : new SendVerificationCodeResult(SendVerificationCodeResultType.SendsFailure);
        //}

        //public virtual async Task<IdentityUserDto> RegisterAsync(RegisterWithPhoneNumberInput input)
        //{
        //    var result = await GetValidateResultAsync(input.PhoneNumber, input.VerificationCode, VerificationCodeType.Register);

        //    if (!result)
        //    {
        //        throw new InvalidVerificationCodeException();
        //    }

        //    var identityUser = await _phoneNumberLoginOrNewUserCreator.CreateAsync(input.PhoneNumber, input.UserName, input.Password, input.EmailAddress);

        //    return ObjectMapper.Map<IdentityUser, IdentityUserDto>(identityUser);
        //}

        //public virtual async Task<ConfirmPhoneNumberResult> ConfirmPhoneNumberAsync(ConfirmPhoneNumberInput input)
        //{

        //    var result = await GetValidateResultAsync(input.PhoneNumber, input.VerificationCode, VerificationCodeType.Register);

        //    if (!result)
        //    {
        //        return new ConfirmPhoneNumberResult(ConfirmPhoneNumberResultType.WrongVerificationCode);
        //    }

        //    var identityUser = await _userRepository.GetByConfirmedPhoneNumberAsync(input.PhoneNumber);

        //    identityUser.SetPhoneNumberConfirmed(true);

        //    (await _identityUserManager.UpdateAsync(identityUser)).CheckErrors();

        //    return new ConfirmPhoneNumberResult(ConfirmPhoneNumberResultType.Success);
        //}

        public virtual async Task ResetPasswordAsync(ResetPasswordWithPhoneNumberInput input)
        {
            await _identityOptions.SetAsync();

            var identityUser = await _userRepository.GetByConfirmedPhoneNumberAsync(input.PhoneNumber);

            (await _identityUserManager.ResetPasswordAsync(identityUser, input.VerificationCode, input.Password)).CheckErrors();
        }

        //public virtual async Task<TryRegisterAndRequestTokenResult> TryRegisterAndRequestTokenAsync(TryRegisterAndRequestTokenInput input)
        //{
        //    var result = await GetValidateResultAsync(input.PhoneNumber, input.VerificationCode, VerificationCodeType.Register);

        //    if (!result)
        //    {
        //        throw new InvalidVerificationCodeException();
        //    }

        //    await _identityOptions.SetAsync();

        //    var registerUser = false;

        //    var identityUser =
        //        await _userRepository.FindByConfirmedPhoneNumberAsync(input.PhoneNumber);

        //    if (identityUser is null)
        //    {
        //        using (var uow = UnitOfWorkManager.Begin(new AbpUnitOfWorkOptions(true), true))
        //        {
        //            identityUser = await _phoneNumberLoginOrNewUserCreator.CreateAsync(input.PhoneNumber, input.UserName, input.Password, input.EmailAddress);

        //            await uow.CompleteAsync();
        //        }

        //        registerUser = true;
        //    }

        //    string code = await GenerateCodeAsync(input.PhoneNumber, VerificationCodeType.Login, identityUser);

        //    return new TryRegisterAndRequestTokenResult(
        //        registerUser ? RegisterResult.RegistrationSuccess : RegisterResult.UserAlreadyExists,
        //        (await RequestAuthServerLoginByCodeAsync(input.PhoneNumber, code))?.Raw,
        //        CurrentTenant.Id);
        //}
        [Route("api/account/phone/requestTokenByPassword")]
        public virtual async Task<IdentityServerRawResultDto> RequestTokenByPasswordAsync(RequestTokenByPasswordInput input)
        {
            //throw new AbpValidationException("password is required");
            var res = new IdentityServerRawResultDto();
            var cres = await RequestAuthServerLoginByPasswordAsync(input.PhoneNumber, input.Password);
            if (cres != null)
            {

                if (cres.IsError)
                {
                    res.IsError = true;
                    res.ErrorDescription = cres.ErrorDescription;
                    if (cres.IsError && cres.Error == "invalid_grant")
                        res.Status = (int)HttpStatusCode.Unauthorized;
                    else if (cres.HttpStatusCode == HttpStatusCode.BadRequest)
                        res.Status = (int)HttpStatusCode.Unauthorized;
                    else
                    {
                        res.Status = (int)cres.HttpStatusCode;
                    }
                }
                else
                {
                    res.Status = (int)cres.HttpStatusCode;
                    res.IsError = false;
                    res.ErrorDescription = null;
                    res.Token = new AccessTokenDto()
                    {
                        AccessToken = cres.AccessToken,
                        RefreshToken = cres.RefreshToken,
                        ExpireIn = cres.ExpiresIn
                    };
                }
            }
            else
            {
                res.IsError = true;
                res.Status = 500;

            }

            if (res.Status == 200)
            {
                _cookieHelper.AddCookie("token", res.Token.AccessToken, 1, ICookieHelper.TimeSpanExpiration.day);
            }

            return res;


        }

        [Route("api/account/phone/requestTokenByVerificationCode")]
        public virtual async Task<IdentityServerRawResultDto> RequestTokenByVerificationCodeAsync(RequestTokenByVerificationCodeInput input)
        {
            var res = new IdentityServerRawResultDto();
            var cres = await RequestAuthServerLoginByCodeAsync(input.PhoneNumber, input.VerificationCode);
            if (cres != null)
            {

                if (cres.IsError)
                {
                    res.IsError = true;
                    res.ErrorDescription = cres.ErrorDescription;
                    if (cres.IsError && cres.Error == "invalid_grant")
                        res.Status = (int)HttpStatusCode.Unauthorized;
                    else if (cres.HttpStatusCode == HttpStatusCode.BadRequest)
                        res.Status = (int)HttpStatusCode.Unauthorized;
                    else
                    {
                        res.Status = (int)cres.HttpStatusCode;
                    }
                }
                else
                {
                    res.Status = (int)cres.HttpStatusCode;
                    res.IsError = false;
                    res.ErrorDescription = null;
                    res.Token = new AccessTokenDto()
                    {
                        AccessToken = cres.AccessToken,
                        RefreshToken = cres.RefreshToken,
                        ExpireIn = cres.ExpiresIn
                    };
                }
            }
            else
            {
                res.IsError = true;
                res.Status = 500;

            }

            return res;


        }
        [Route("api/account/phone/refreshToken")]
        public virtual async Task<IdentityServerRawResultDto> RefreshTokenAsync(RefreshTokenInput input)
        {
            var res = new IdentityServerRawResultDto();
            var cres = await RequestAuthServerRefreshAsync(input.RefreshToken);
            if (cres != null)
            {

                if (cres.IsError)
                {
                    res.IsError = true;
                    res.ErrorDescription = cres.ErrorDescription;
                    if (cres.IsError && cres.Error == "invalid_grant")
                        res.Status = (int)HttpStatusCode.Unauthorized;
                    else if (cres.HttpStatusCode == HttpStatusCode.BadRequest)
                        res.Status = (int)HttpStatusCode.Unauthorized;
                    else
                    {
                        res.Status = (int)cres.HttpStatusCode;
                    }
                }
                else
                {
                    res.Status = (int)cres.HttpStatusCode;
                    res.IsError = false;
                    res.ErrorDescription = null;
                    res.Token = new AccessTokenDto()
                    {
                        AccessToken = cres.AccessToken,
                        RefreshToken = cres.RefreshToken,
                        ExpireIn = cres.ExpiresIn
                    };
                }
            }
            else
            {
                res.IsError = true;
                res.Status = 500;

            }
            return res;
        }

        //protected virtual async Task<bool> GetValidateResultAsync(string phoneNumber, string code, VerificationCodeType type)
        //{

        //    switch (type)
        //    {
        //        case VerificationCodeType.ResetPassword:

        //            // Not able to validate reset password token here using default asp.net identity implementation

        //            return true;

        //        case VerificationCodeType.Register:

        //            return await _verificationCodeManager.ValidateAsync(
        //                codeCacheKey: $"{PhoneNumberLoginConsts.VerificationCodeCachePrefix}:{type}:{phoneNumber}",
        //                verificationCode: code,
        //                configuration: new VerificationCodeConfiguration());

        //        case VerificationCodeType.Login:

        //            var loginUser = await _userRepository.GetByConfirmedPhoneNumberAsync(phoneNumber);

        //            return await _identityUserManager.VerifyUserTokenAsync(loginUser, TokenOptions.DefaultPhoneProvider, PhoneNumberLoginConsts.LoginPurposeName, code);

        //        case VerificationCodeType.Confirm:

        //            var confirmPhoneUser = await _userRepository.GetByConfirmedPhoneNumberAsync(phoneNumber);

        //            return await _identityUserManager.VerifyUserTokenAsync(confirmPhoneUser, TokenOptions.DefaultPhoneProvider, PhoneNumberLoginConsts.ConfirmPurposeName, code);
        //    }

        //    return false;

        //}

        protected virtual async Task<TokenResponse> RequestAuthServerLoginByCodeAsync(string phoneNumber, string code)
        {
            var client = _httpClientFactory.CreateClient(PhoneNumberLoginConsts.IdentityServerHttpClientName);
            var authServerAddress = _configuration["AuthServer:Authority"].EndsWith("/")
                ? _configuration["AuthServer:Authority"].Substring(0, _configuration["AuthServer:Authority"].Length - 1)
                : _configuration["AuthServer:Authority"];
            var request = new TokenRequest
            {

                Address = authServerAddress + "/connect/token",
                GrantType = PhoneNumberLoginConsts.GrantType,

                ClientId = _configuration["AuthServer:ClientId"],
                ClientSecret = _configuration["AuthServer:ClientSecret"],

                Parameters =
                {
                    {"phone_number", phoneNumber},
                    {"code", code}
                }
            };

            request.Headers.Add(GetTenantHeaderName(), CurrentTenant.Id?.ToString());

            var res = await client.RequestTokenAsync(request);

            return res;
        }

        protected virtual async Task<TokenResponse> RequestAuthServerLoginByPasswordAsync(string phoneNumber, string password)
        {
            var client = _httpClientFactory.CreateClient(PhoneNumberLoginConsts.IdentityServerHttpClientName);
            var authServerAddress = _configuration["AuthServer:Authority"].EndsWith("/")
                ? _configuration["AuthServer:Authority"].Substring(0, _configuration["AuthServer:Authority"].Length - 1)
                : _configuration["AuthServer:Authority"];
            var request = new TokenRequest
            {

                Address = authServerAddress + "/connect/token",
                GrantType = PhoneNumberLoginConsts.GrantType,

                ClientId = _configuration["AuthServer:ClientId"],
                ClientSecret = _configuration["AuthServer:ClientSecret"],

                Parameters =
                {
                    {"phone_number", phoneNumber},
                    {"password", password}
                }
            };
            _logger.Log(LogLevel.Warning, request.ClientId + " : " + request.ClientSecret);
            request.Headers.Add(GetTenantHeaderName(), CurrentTenant.Id?.ToString());

            return await client.RequestTokenAsync(request);
        }

        protected virtual async Task<TokenResponse> RequestAuthServerRefreshAsync(string refreshToken)
        {
            var client = _httpClientFactory.CreateClient(PhoneNumberLoginConsts.IdentityServerHttpClientName);
            var authServerAddress = _configuration["AuthServer:Authority"].EndsWith("/")
                ? _configuration["AuthServer:Authority"].Substring(0, _configuration["AuthServer:Authority"].Length - 1)
                : _configuration["AuthServer:Authority"];
            var request = new RefreshTokenRequest
            {
                Address = authServerAddress + "/connect/token",

                ClientId = _configuration["AuthServer:ClientId"],
                ClientSecret = _configuration["AuthServer:ClientSecret"],

                RefreshToken = refreshToken
            };

            request.Headers.Add(GetTenantHeaderName(), CurrentTenant.Id?.ToString());

            return await client.RequestRefreshTokenAsync(request);
        }
        private string GetConfigurationOrDefault<T>(IConfiguration configuration, string configurationName, T defaultValue = default)
        {
            if (configuration[configurationName] != null)
                return configuration[configurationName];
            else
            {
                return defaultValue == null ? null : defaultValue.ToString();
            }
        }
        protected virtual string GetTenantHeaderName()
        {
            return "__tenant";
        }

        protected virtual async Task<int> GetRegisterCodeCacheSecondsAsync()
        {
            return await _settingProvider.GetAsync<int>(IdentityServiceSettingNames.RegisterCodeCacheSeconds);
        }



        //protected virtual async Task<string> GenerateCodeAsync(string phoneNumber, VerificationCodeType type, IdentityUser identityUser = null)
        //{
        //    var code = string.Empty;

        //    switch (type)
        //    {
        //        case VerificationCodeType.ResetPassword:
        //            code = await _identityUserManager.GeneratePasswordResetTokenAsync(identityUser);
        //            break;

        //        case VerificationCodeType.Register:

        //            code = await _verificationCodeManager.GenerateAsync(
        //                codeCacheKey: $"{PhoneNumberLoginConsts.VerificationCodeCachePrefix}:{type}:{phoneNumber}",
        //                codeCacheLifespan: TimeSpan.FromSeconds(await GetRegisterCodeCacheSecondsAsync()),
        //                configuration: new VerificationCodeConfiguration());
        //            break;

        //        case VerificationCodeType.Login:

        //            code = await _identityUserManager.GenerateUserTokenAsync(identityUser, TokenOptions.DefaultPhoneProvider, PhoneNumberLoginConsts.LoginPurposeName);
        //            break;

        //        case VerificationCodeType.Confirm:

        //            code = await _identityUserManager.GenerateUserTokenAsync(identityUser, TokenOptions.DefaultPhoneProvider, PhoneNumberLoginConsts.ConfirmPurposeName);
        //            break;
        //        case VerificationCodeType.SignInCode:

        //            code = await _identityUserManager.GenerateTwoFactorTokenAsync(identityUser, TokenOptions.DefaultPhoneProvider);
        //            break;
        //    }

        //    return code;
        //}
    }
}