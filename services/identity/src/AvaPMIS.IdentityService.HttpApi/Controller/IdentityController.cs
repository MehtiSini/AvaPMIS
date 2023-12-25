using System;
using System.Net;
using System.Threading.Tasks;
using AvaPMIS.IdentityService.Account;
using AvaPMIS.IdentityService.Account.Dtos;
using AvaPMIS.IdentityService.Cookie;
using AvaPMIS.IdentityService.Identity;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Users;

namespace AvaPMIS.IdentityService.Controllers
{
    [Route("api/identity")]
    public class IdentityController : AbpController, ITransientDependency
    {
        IPhoneNumberLoginAccountAppService _phoneNumberLoginAccountAppService;
        IAdminUserManagerAppService _adminUserManagerAppService;
        private readonly ICurrentUser _currentUser;

        public IdentityController(IPhoneNumberLoginAccountAppService phoneNumberLoginAccountAppService, IAdminUserManagerAppService adminUserManagerAppService, ICurrentUser currentUser)
        {
            _phoneNumberLoginAccountAppService = phoneNumberLoginAccountAppService;
            _adminUserManagerAppService = adminUserManagerAppService;
            _currentUser = currentUser;
        }

        #region Phone

        [Route("phone/sendSignInCode")]
        [HttpPost]
        public async virtual Task<ActionResult> SendSignInCodeAsync([FromBody] SendPhoneOtpCodeDto input)
        {

            var res = await _phoneNumberLoginAccountAppService.SendSignInCodeAsync(input);
            var jres = new ObjectResult(res);
            jres.StatusCode = res.HttpStatus;

            return jres;
        }

        [HttpPost]
        [Route("phone/requestTokenByPassword")]
        public virtual async Task<ActionResult> RequestTokenByPasswordAsync(
            [FromBody] RequestTokenByPasswordInput input)
        {
            var res = await _phoneNumberLoginAccountAppService.RequestTokenByPasswordAsync(input);
            var jres = new ObjectResult(res);
            jres.StatusCode = res.Status;
            return jres;
        }

        [HttpPost]
        [Route("phone/requestTokenByVerificationCode")]
        public virtual async Task<ActionResult> RequestTokenByByVerificationCodeAsync([FromBody] RequestTokenByVerificationCodeInput input)
        {
            //try
            //{
            var res = await _phoneNumberLoginAccountAppService.RequestTokenByVerificationCodeAsync(input);
            var jres = new ObjectResult(res);
            jres.StatusCode = res.Status;
            return jres;
            //if (res.IsError)
            //{
            //    return Unauthorized(res.RawResult);
            //}
            //else
            //{
            //    return Ok(res.RawResult);
            //}
            //}
            //catch (Exception e)
            //{
            //    var jres = new ObjectResult(e);
            //    jres.StatusCode = (int)(HttpStatusCode.Unauthorized);
            //    return jres;
            //}

        }

        [HttpPost]
        [Route("phone/refreshToken")]
        public virtual async Task<ActionResult> RefreshTokenAsync([FromBody] RefreshTokenInput input)
        {
            //try
            //{
            var res = await _phoneNumberLoginAccountAppService.RefreshTokenAsync(input);
            var jres = new JsonResult(res);
            jres.StatusCode = res.Status;
            return jres;
            //if (res.IsError)
            //{
            //    return Unauthorized(res.RawResult);
            //}
            //else
            //{
            //    return Ok(res.RawResult);
            //}
            //}
            //catch (Exception e)
            //{
            //    var jres = new ObjectResult(e);
            //    jres.StatusCode = (int)(HttpStatusCode.Unauthorized);
            //    return jres;
            //}

        }
        #endregion




    }
}