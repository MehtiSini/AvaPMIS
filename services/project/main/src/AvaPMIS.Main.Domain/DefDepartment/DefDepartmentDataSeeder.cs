using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace AvaPMIS.Main.DefDepartment
{
    public class DefDepartmentDataSeeder : IDefDepartmentDataSeeder, ITransientDependency
    {
        private readonly IDefDepartmentRepository _defDepartmentRepository;

        public DefDepartmentDataSeeder(IDefDepartmentRepository defDepartmentRepository)
        {
            _defDepartmentRepository = defDepartmentRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            var defDepartments = await _defDepartmentRepository.GetListAsync();

            if (defDepartments.Count == 0)
            {
                var entities = new List<DefDepartment>
                {
                    new DefDepartment { Name = "Department 1", Code = "D1" },
                    new DefDepartment { Name = "Department 2", Code = "D2" },
                    new DefDepartment { Name = "Department 3", Code = "D3" },
                    new DefDepartment { Name = "Department 4", Code = "D4" },
                    new DefDepartment { Name = "Department 5", Code = "D5" }
                };

                await _defDepartmentRepository.InsertManyAsync(entities, true);
            }
        }
    }
}
