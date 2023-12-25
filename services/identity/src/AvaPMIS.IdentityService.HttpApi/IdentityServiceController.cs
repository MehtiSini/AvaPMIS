using AvaPMIS.IdentityService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace AvaPMIS.IdentityService;

public abstract class IdentityServiceController : AbpControllerBase
{
    protected IdentityServiceController()
    {
        LocalizationResource = typeof(IdentityServiceResource);
    }
}