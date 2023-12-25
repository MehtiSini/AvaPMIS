namespace AvaPMIS.IdentityService.Settings;

public static class IdentityServiceSettingNames
{
    private const string Prefix = "IdentityService";

    public const string RegisterCodeCacheSeconds = Prefix + ".RegisterCodeCacheSeconds";
    public const string SmsRepetInterval = Prefix + ".PhoneNumberLogin.SmsRepetInterval";
    public const string SmsUserSignin = Prefix + ".PhoneNumberLogin.SmsUserSignin";
}