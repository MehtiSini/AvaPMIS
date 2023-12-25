using Volo.Abp.Sms;

namespace Nozhan.Abp.Utilities.SMSSender
{
    public interface ISimpleMessageSender
    {
        Task<bool> SendSMSAsync(SmsMessage smsMessage);
    }
}