using Ghasedak.Core;
using Ghasedak.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Nozhan.Abp.Utilities.SMSSender;
using Volo.Abp.Modularity;
using Volo.Abp.Settings;
using Volo.Abp.Sms;

namespace Nozhan.Abp.Utilities;
[DependsOn(typeof(AbpSmsModule))]
[DependsOn(typeof(AbpSettingsModule))]
public class NozhanUtilitiesModule : AbpModule
{
    
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var apiKey= configuration.GetValue("SMS:ApiKey", "3456a87e79ecccac5971315c83889f95d0e97135745474a0a78ad2a5e21716d1");
        //Register an instance as singleton
        context.Services.AddSingleton<ISMSService>(new Api(apiKey));
        var logger = context.Services.GetInitLogger<NozhanUtilitiesModule>();
        logger.LogInformation("SMS Api Key="+apiKey);
        //Replacing the IConnectionStringResolver service
        context.Services.Replace(
            ServiceDescriptor.Transient<
                ISmsSender,
                GhasedakSMSSender
            >());
        context.Services.Replace(
            ServiceDescriptor.Transient<
                ISimpleMessageSender,
                GhasedakSMSSender
            >());
    }
}