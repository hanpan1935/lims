using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.Lims.Calibrations;

public class CalibrationRepository : EfCoreRepository<LimsDbContext, Calibration, Guid>, ICalibrationRepository
{
    public CalibrationRepository(IDbContextProvider<LimsDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Calibration>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}