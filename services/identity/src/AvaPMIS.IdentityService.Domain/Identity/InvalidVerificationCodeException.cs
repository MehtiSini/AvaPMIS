using Volo.Abp;

namespace AvaPMIS.IdentityService.Identity
{
    public class InvalidVerificationCodeException : BusinessException
    {
        public InvalidVerificationCodeException() : base(PhoneNumberLoginErrorCodes.InvalidVerificationCode)
        {

        }
    }
}