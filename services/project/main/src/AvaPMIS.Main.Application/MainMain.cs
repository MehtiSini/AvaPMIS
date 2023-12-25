using AvaPMIS.Main.Localization;
using Volo.Abp.Application.Services;

namespace AvaPMIS.Main;

public abstract class MainMain : ApplicationService
{
    protected MainMain()
    {
        LocalizationResource = typeof(MainResource);
        ObjectMapperContext = typeof(MainApplicationModule);
    }
}