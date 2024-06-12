using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.Lims.Calibrations;

/// <summary>
/// 
/// </summary>
public interface ICalibrationRepository : IRepository<Calibration, Guid>
{
}
