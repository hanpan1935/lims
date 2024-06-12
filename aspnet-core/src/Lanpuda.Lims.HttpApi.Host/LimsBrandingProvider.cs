using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Lanpuda.Lims;

[Dependency(ReplaceServices = true)]
public class LimsBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Lims";
}
