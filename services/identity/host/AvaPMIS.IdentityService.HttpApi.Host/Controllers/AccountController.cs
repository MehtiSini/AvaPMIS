﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Account;
using Volo.Abp.Identity;
//using IAccountAppService = AvaPMIS.IdentityService.Account.IPhoneAccountAppService;

namespace Lazy.Abp.Account
{
    //[RemoteService(Name = AccountRemoteServiceConsts.RemoteServiceName)]
    //[Area("account")]
    //[Route("api/account")]
    //public class AccountController : AbpController, IAccountAppService
    //{
    //    protected IAccountAppService AccountAppService { get; }
    //    public AccountController(
    //        IAccountAppService accountAppService)
    //    {
    //        AccountAppService = accountAppService;
    //    }

    //    [HttpPost]
    //    [Route("phone/register")]
    //    public virtual async Task RegisterAsync(PhoneRegisterDto input)
    //    {
    //        await AccountAppService.RegisterAsync(input);
    //    }

    //    [HttpPut]
    //    [Route("phone/reset-password")]
    //    public virtual async Task ResetPasswordAsync(PhoneResetPasswordDto input)
    //    {
    //        await AccountAppService.ResetPasswordAsync(input);
    //    }

    //    [HttpPost]
    //    [Route("phone/send-signin-code")]
    //    public virtual async Task SendPhoneSigninCodeAsync(SendPhoneSigninCodeDto input)
    //    {
    //        await AccountAppService.SendPhoneSigninCodeAsync(input);
    //    }

    //    [HttpPost]
    //    [Route("phone/send-register-code")]
    //    public virtual async Task SendPhoneRegisterCodeAsync(SendPhoneRegisterCodeDto input)
    //    {
    //        await AccountAppService.SendPhoneRegisterCodeAsync(input);
    //    }

    //    [HttpPost]
    //    [Route("phone/send-password-reset-code")]
    //    public virtual async Task SendPhoneResetPasswordCodeAsync(SendPhoneResetPasswordCodeDto input)
    //    {
    //        await AccountAppService.SendPhoneResetPasswordCodeAsync(input);
    //    }


    //    [HttpPost]
    //    [Route("email/register")]
    //    public Task<IdentityUserDto> RegisterAsync(RegisterDto input)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task SendPasswordResetCodeAsync(SendPasswordResetCodeDto input)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<bool> VerifyPasswordResetTokenAsync(VerifyPasswordResetTokenInput input)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task ResetPasswordAsync(ResetPasswordDto input)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
