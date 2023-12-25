using Nozhan.Abp.Utilities.SMSSender;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Sms;

namespace Nozhan.Abp.Utilities.Identity
{
    public class DefaultUserSecurityCodeSender:IUserSecurityCodeSender,ITransientDependency
    {
        private readonly ISimpleMessageSender _messageSender;

        public DefaultUserSecurityCodeSender(ISimpleMessageSender messageSender)
        {
            _messageSender = messageSender;
        }

        public async Task<bool> SendAsync(VerificationSendingMethod method, string receivr, string template, string code,
            VerificationCodeType type, object textTemplateModel = null)
        {
            var message = template;
            if (string.IsNullOrEmpty(message))
            {
                message = "{0}";
            }

            message = string.Format(message, code);
            var smsMessage = new SmsMessage(receivr, message);
            var res=await _messageSender.SendSMSAsync(smsMessage);
            return res;
        }
    }
}