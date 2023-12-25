using AvaPMIS.SaaS.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace AvaPMIS.SaaS;

public abstract class SaaSController : AbpControllerBase
{
    protected SaaSController()
    {
        LocalizationResource = typeof(SaaSResource);
    }
}