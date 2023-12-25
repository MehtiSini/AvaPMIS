using AvaPMIS.Main.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace AvaPMIS.Main.Settings;

public class MainSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //context.Add(new SettingDefinition(
        //    MainSettings.RegisterCodeCacheSeconds,
        //    "300",
        //    L("RegisterCodeCacheSeconds")));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MainResource>(name);
    }
}