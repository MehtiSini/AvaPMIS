using AvaPMIS.Administration.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace AvaPMIS.Administration;

public abstract class AdministrationController : AbpControllerBase
{
    protected AdministrationController()
    {
        LocalizationResource = typeof(AdministrationResource);
    }
}