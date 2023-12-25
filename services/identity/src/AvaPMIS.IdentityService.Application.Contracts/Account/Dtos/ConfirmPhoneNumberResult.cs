using System;

namespace AvaPMIS.IdentityService.Account.Dtos
{
    [Serializable]
    public class ConfirmPhoneNumberResult
    {
        public ConfirmPhoneNumberResultType Result { get; }

        public string Description => Result.ToString();

        public ConfirmPhoneNumberResult(ConfirmPhoneNumberResultType result)
        {
            Result = result;
        }
    }
}