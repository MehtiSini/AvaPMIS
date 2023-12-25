using System;
using System.ComponentModel.DataAnnotations;
using AvaPMIS.IdentityService.Identity;
using Volo.Abp.Identity;
using Volo.Abp.Validation;

namespace AvaPMIS.IdentityService.Account.Dtos
{
    [Serializable]
    public class ConfirmPhoneNumberInput
    {
        [Required]
        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxPhoneNumberLength))]
        public string PhoneNumber { get; set; }
        
        [Required]
        [DynamicStringLength(typeof(PhoneNumberLoginConsts), nameof(PhoneNumberLoginConsts.MaxVerificationCodeLength))]
        public string VerificationCode { get; set; }
    }
}