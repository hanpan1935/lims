using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.UniqueCode
{
    public class UniqueCodeGenerator : IUniqueCodeGenerator
    {
        private readonly IConfiguration Configuration;
        public UniqueCodeGenerator(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<string> GetUniqueNumberAsync(string prefix)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(Configuration["Redis:Configuration"]);
            IDatabase db = redis.GetDatabase();

            var res = await db.StringIncrementAsync(prefix);
            DateTime start = DateTime.Now;
            DateTime end = new DateTime(start.Year, start.Month, start.Day, 23, 59, 59);
            TimeSpan span = end - start;
            await db.KeyExpireAsync(prefix, span);

            string resStr = res.ToString();
            switch (resStr.Length)
            {
                case 1:
                    resStr = "0000" + resStr;
                    break;
                case 2:
                    resStr = "000" + resStr;
                    break;
                case 3:
                    resStr = "00" + resStr;
                    break;
                case 4:
                    resStr = "0" + resStr;
                    break;
                case 5:
                    break;
                default:
                    throw new Exception("只能生成5位数的唯一编码");
            }

            string month = start.Month.ToString();
            if (month.Length == 1)
            {
                month = "0" + month;
            }
            string day = start.Day.ToString();
            if (day.Length == 1)
            {
                day = "0" + day;
            }

            string result = prefix + start.Year + month + day + resStr;

            return result;
        }
    }
}
