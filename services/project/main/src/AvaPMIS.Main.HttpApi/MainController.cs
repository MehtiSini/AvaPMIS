using AvaPMIS.Main.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace AvaPMIS.Main;

public abstract class MainController : AbpControllerBase
{
    protected MainController()
    {
        LocalizationResource = typeof(MainResource);
    }
}