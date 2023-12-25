using System;
using System.Net;
using AvaPMIS.IdentityService.Identity;

namespace AvaPMIS.IdentityService.Account.Dtos
{
    [Serializable]
    public class SendSignInCodeResult
    {
        public int HttpStatus { get; set; }
        public SendSignInCodeResultType Result { get; }

        public string Description => Result.ToString();
        public FindOrConfirmOrCreateStatus IdentityStatus { get; set; } = FindOrConfirmOrCreateStatus.None;
        public SendSignInCodeResult(SendSignInCodeResultType result, FindOrConfirmOrCreateStatus identityStatus=FindOrConfirmOrCreateStatus.None)
        {
            Result = result;
            switch (result)
            {
                case SendSignInCodeResultType.Success:
                    HttpStatus = 200;
                    break;
                case SendSignInCodeResultType.SendsFailure:
                    HttpStatus = 200;
                    break;
                case SendSignInCodeResultType.FrequencyIsLimited:
                    HttpStatus = (int)HttpStatusCode.TooManyRequests;
                    break;
            }

            IdentityStatus = identityStatus;
        }
    }
}