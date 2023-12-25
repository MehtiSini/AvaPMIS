using System.Threading.Tasks;
using AvaPMIS.IdentityService.Account.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace AvaPMIS.IdentityService.Account
{
    public interface IPhoneNumberLoginAccountAppService : IApplicationService
    {
        //Task<SendVerificationCodeResult> SendVerificationCodeAsync(SendVerificationCodeInput input);

        //Task<IdentityUserDto> RegisterAsync(RegisterWithPhoneNumberInput input);

        //Task<ConfirmPhoneNumberResult> ConfirmPhoneNumberAsync(ConfirmPhoneNumberInput input);

        //Task ResetPasswordAsync(ResetPasswordWithPhoneNumberInput input);
        Task<SendSignInCodeResult> SendSignInCodeAsync(SendPhoneOtpCodeDto input);
        Task<IdentityServerRawResultDto> RequestTokenByPasswordAsync(RequestTokenByPasswordInput input);

        Task<IdentityServerRawResultDto> RequestTokenByVerificationCodeAsync(RequestTokenByVerificationCodeInput input);

        Task<IdentityServerRawResultDto> RefreshTokenAsync(RefreshTokenInput input);

        //Task<TryRegisterAndRequestTokenResult> TryRegisterAndRequestTokenAsync(TryRegisterAndRequestTokenInput input);
    }
}