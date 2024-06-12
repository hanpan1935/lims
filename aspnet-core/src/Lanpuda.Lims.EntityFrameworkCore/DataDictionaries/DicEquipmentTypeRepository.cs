using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.Lims.DataDictionaries;

public class DicEquipmentTypeRepository : EfCoreRepository<LimsDbContext, DicEquipmentType, int>, IDicEquipmentTypeRepository
{
    public DicEquipmentTypeRepository(IDbContextProvider<LimsDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<DicEquipmentType>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}