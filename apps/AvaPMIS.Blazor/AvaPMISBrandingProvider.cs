using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace AvaPMIS.Blazor;

[Dependency(ReplaceServices = true)]
public class AvaPMISBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "AvaPMIS";
}
