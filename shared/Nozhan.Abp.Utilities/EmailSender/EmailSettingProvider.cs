using Volo.Abp.Settings;

namespace Nozhan.Abp.Utilities.EmailSender
{
    public class EmailSettingProvider : SettingDefinitionProvider
    {
        private readonly ISettingValueProvider _settingValueProvider;

        //Inject ISettingProvider in the constructor
        public EmailSettingProvider(ConfigurationSettingValueProvider settingValueProvider)
        {
            _settingValueProvider = settingValueProvider;
        }
        public override void Define(ISettingDefinitionContext context)
        {
            context.Add(
                new SettingDefinition("Smtp.Host", "127.0.0.1"),
                new SettingDefinition("Smtp.Port", "25"),
                new SettingDefinition("Smtp.UserName"),
                new SettingDefinition("Smtp.Password", isEncrypted: true),
                new SettingDefinition("Smtp.EnableSsl", "false")
            );
        }
    }
}