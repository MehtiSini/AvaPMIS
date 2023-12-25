using AvaPMIS.Administration.Localization;
using Volo.Abp.Application.Services;

namespace AvaPMIS.Administration;

public abstract class AdministrationAppService : ApplicationService
{
    protected AdministrationAppService()
    {
        LocalizationResource = typeof(AdministrationResource);
        ObjectMapperContext = typeof(AdministrationApplicationModule);
    }
}