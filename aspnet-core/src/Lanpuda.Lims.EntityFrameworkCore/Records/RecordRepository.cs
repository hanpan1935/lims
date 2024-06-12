using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Lanpuda.Lims.Records;

public class RecordRepository : EfCoreRepository<LimsDbContext, Record, Guid>, IRecordRepository
{
    public RecordRepository(IDbContextProvider<LimsDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Record>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}