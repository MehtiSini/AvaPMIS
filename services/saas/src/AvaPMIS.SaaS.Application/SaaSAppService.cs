using AvaPMIS.SaaS.Localization;
using Volo.Abp.Application.Services;

namespace AvaPMIS.SaaS;

public abstract class SaaSAppService : ApplicationService
{
    protected SaaSAppService()
    {
        LocalizationResource = typeof(SaaSResource);
        ObjectMapperContext = typeof(SaaSApplicationModule);
    }
}