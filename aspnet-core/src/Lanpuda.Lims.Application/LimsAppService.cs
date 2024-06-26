﻿using System;
using System.Collections.Generic;
using System.Text;
using Lanpuda.Lims.Localization;
using Volo.Abp.Application.Services;

namespace Lanpuda.Lims;

/* Inherit your application services from this class.
 */
public abstract class LimsAppService : ApplicationService
{
    protected LimsAppService()
    {
        LocalizationResource = typeof(LimsResource);
    }
}
