using System.Threading.Tasks;

namespace Lanpuda.Lims.Data;

public interface ILimsDbSchemaMigrator
{
    Task MigrateAsync();
}
