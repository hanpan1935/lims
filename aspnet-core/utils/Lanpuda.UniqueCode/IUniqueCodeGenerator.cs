using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Lanpuda.UniqueCode
{
    public interface IUniqueCodeGenerator : ITransientDependency
    {
        Task<string> GetUniqueNumberAsync(string prefix);
    }
}
