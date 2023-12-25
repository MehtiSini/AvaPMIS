namespace AvaPMIS.IdentityService.Identity
{
    public static class PhoneNumberLoginConsts
    {
        public static int MaxVerificationCodeLength { get; set; } = 8;

        public const string GrantType = "PhoneNumberLogin_credentials";

        public const string IdentityServerHttpClientName = "IdentityServicePhoneNumberLogin";

        public const string VerificationCodeCachePrefix = "PhoneNumberLoginVerificationCode";

        public const string LoginPurposeName = "LoginByPhoneNumber";

        public const string ConfirmPurposeName = "ConfirmPhoneNumber";
    }
}