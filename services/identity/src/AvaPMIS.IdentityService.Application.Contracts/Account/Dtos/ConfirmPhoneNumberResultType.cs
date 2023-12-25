namespace AvaPMIS.IdentityService.Account.Dtos
{
    public enum ConfirmPhoneNumberResultType : byte
    {
        Success = 1,

        DuplicateConfirmedPhoneNumber = 2,
        
        WrongVerificationCode = 3
    }
}