using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace AvaPMIS;

[Dependency(ReplaceServices = true)]
public class AvaPMISBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "AvaPMIS";
}
