using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.Lims.Equipments;

public class EquipmentRepository : EfCoreRepository<LimsDbContext, Equipment, Guid>, IEquipmentRepository
{
    public EquipmentRepository(IDbContextProvider<LimsDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Equipment>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}