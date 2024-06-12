using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.Lims.Records;

public class RecordDetailRepository : EfCoreRepository<LimsDbContext, RecordDetail, Guid>, IRecordDetailRepository
{
    public RecordDetailRepository(IDbContextProvider<LimsDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<RecordDetail>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}