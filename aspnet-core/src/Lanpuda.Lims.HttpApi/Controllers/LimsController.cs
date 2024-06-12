using Lanpuda.Lims.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Lanpuda.Lims.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class LimsController : AbpControllerBase
{
    protected LimsController()
    {
        LocalizationResource = typeof(LimsResource);
    }
}
