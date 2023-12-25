using System.Collections.Generic;
using System.Threading.Tasks;
using AvaPMIS.Main.Discipline;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace AvaPMIS.Main.DepartmentDiscipline
{
    public class DepartmentDisciplineDataSeeder : IDepartmentDisciplineDataSeeder, ITransientDependency
    {
        private readonly IDepartmentDisciplineRepository _disciplineRepository;
        private readonly IDepartmentDisciplineRepository _departmentRepository;

        public DepartmentDisciplineDataSeeder(IDepartmentDisciplineRepository disciplineRepository, IDepartmentDisciplineRepository departmentRepository)
        {
            _disciplineRepository = disciplineRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            var disciplines = await _disciplineRepository.GetListAsync();

            if (disciplines.Count == 0)
            {
                var departments = await _departmentRepository.GetListAsync();

                var entities = new List<DepartmentDiscipline>
                {
                    new() {
                        DepartmentId = departments[0].Id,
                        Code = "Discipline001"
                    },
                    new() {
                        DepartmentId = departments[1].Id,
                        Code = "Discipline002"
                    },
                    new() {
                        DepartmentId = departments[2].Id,
                        Code = "Discipline003"
                    },
                    new() {
                        DepartmentId = departments[3].Id,
                        Code = "Discipline004"
                    },
                    new() {
                        DepartmentId = departments[4].Id,
                        Code = "Discipline005"
                    }
                };

                await _disciplineRepository.InsertManyAsync(entities, true);
            }
        }
    }
}
