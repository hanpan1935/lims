using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.Lims.InspectionTasks;

public class InspectionTaskRepository : EfCoreRepository<LimsDbContext, InspectionTask, Guid>, IInspectionTaskRepository
{
    public InspectionTaskRepository(IDbContextProvider<LimsDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<InspectionTask>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}