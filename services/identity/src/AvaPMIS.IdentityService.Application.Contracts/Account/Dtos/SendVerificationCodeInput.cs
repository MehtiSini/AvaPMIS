using System;
using System.ComponentModel.DataAnnotations;
using AvaPMIS.IdentityService.Identity;
using Nozhan.Abp.Utilities.Identity;
using Volo.Abp.Identity;
using Volo.Abp.Validation;

namespace AvaPMIS.IdentityService.Account.Dtos
{
    [Serializable]
    public class SendVerificationCodeInput
    {
        public VerificationCodeType VerificationCodeType { get; set; }
        
        [Required]
        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxPhoneNumberLength))]
        public string PhoneNumber { get; set; }


    }
}