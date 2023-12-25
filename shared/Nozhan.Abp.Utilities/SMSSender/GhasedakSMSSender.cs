using Ghasedak.Core;
using Ghasedak.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Sms;

namespace Nozhan.Abp.Utilities.SMSSender
{
   
    public class GhasedakSMSSender : ISmsSender, ISimpleMessageSender, ITransientDependency
    {
        private readonly IConfiguration _configuration;
        private readonly ISMSService _smsService;
        private readonly ILogger<GhasedakSMSSender> _logger;

        public GhasedakSMSSender(IConfiguration configuration, ISMSService smsService,ILogger<GhasedakSMSSender> logger)
        {
            _configuration = configuration;
            var apiKey = configuration.GetValue("SMS:ApiKey", "3456a87e79ecccac5971315c83889f95d0e97135745474a0a78ad2a5e21716d1");

            logger.LogInformation("SMS Api Key=" + apiKey);
            _smsService = new Api(apiKey);
            _logger = logger;
        }

        public async Task SendAsync(SmsMessage smsMessage)
        {
            await SendSMSAsync(smsMessage);

        }

        public async Task<bool> SendSMSAsync(SmsMessage smsMessage)
        {
            try
            {
                var options = smsMessage.Properties;
                if (options == null)
                {
                    options = new Dictionary<string, object>();
                }

                string? LineNumber = null;
                if (options.ContainsKey(nameof(LineNumber)))
                    LineNumber = options[nameof(LineNumber)].ToString();
                DateTime? SendDate = null;
                if (options.ContainsKey(nameof(SendDate)))
                    SendDate = (DateTime?)options[nameof(SendDate)];
                string? CheckId = null;
                if (options.ContainsKey(nameof(CheckId)))
                    CheckId = options[nameof(CheckId)].ToString();

                string? Department = null;
                if (options.ContainsKey(nameof(Department)))
                    Department = options[nameof(Department)].ToString();
                var res = await _smsService.SendSMSAsync(smsMessage.Text, smsMessage.PhoneNumber, LineNumber, SendDate, CheckId,
                    Department);
                smsMessage.Properties.Add("Result", res.Result);
                return res.Result.Code == 200;
            }

            catch (Ghasedak.Core.Exceptions.ApiException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            catch (Ghasedak.Core.Exceptions.ConnectionException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }


}