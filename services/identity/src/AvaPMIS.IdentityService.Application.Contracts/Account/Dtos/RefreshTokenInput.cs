using System;
using System.ComponentModel.DataAnnotations;

namespace AvaPMIS.IdentityService.Account.Dtos
{
    [Serializable]
    public class RefreshTokenInput
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}