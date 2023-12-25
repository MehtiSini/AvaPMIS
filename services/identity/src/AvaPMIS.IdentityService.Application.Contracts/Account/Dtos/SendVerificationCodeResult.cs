using System;

namespace AvaPMIS.IdentityService.Account.Dtos
{
    [Serializable]
    public class SendVerificationCodeResult
    {
        public SendVerificationCodeResultType Result { get; }

        public string Description => Result.ToString();

        public SendVerificationCodeResult(SendVerificationCodeResultType result)
        {
            Result = result;
        }
    }
}