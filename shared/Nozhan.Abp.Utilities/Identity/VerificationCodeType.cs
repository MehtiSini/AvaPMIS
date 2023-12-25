namespace Nozhan.Abp.Utilities.Identity
{
    public enum VerificationCodeType : byte
    {
        Login = 1,
        Register = 2,
        ResetPassword = 3,
        Confirm = 4,
        SignInCode=5
    }
}