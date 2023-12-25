using AvaPMIS.IdentityService.Localization;
using Volo.Abp.Application.Services;

namespace AvaPMIS.IdentityService;

public abstract class IdentityServiceAppService : ApplicationService
{
    protected IdentityServiceAppService()
    {
        LocalizationResource = typeof(IdentityServiceResource);
        ObjectMapperContext = typeof(IdentityServiceApplicationModule);
    }
}