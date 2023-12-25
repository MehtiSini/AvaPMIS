using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;

namespace AvaPMIS.Main
{
    public interface IMainDataSeeder
    {
        Task SeedAsync(DataSeedContext context);
    }
}
