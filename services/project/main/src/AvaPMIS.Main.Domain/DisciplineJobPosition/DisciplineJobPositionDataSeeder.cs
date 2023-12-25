using System.Collections.Generic;
using System.Threading.Tasks;
using AvaPMIS.Main.DepartmentDiscipline;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace AvaPMIS.Main.DisciplineJobPosition
{
    public class DisciplineJobPositionDataSeeder : IDisciplineJobPositionDataSeeder, ITransientDependency
    {
        private readonly IDisciplineJobPositionRepository _disciplineJobPositionRepository;
        private readonly IDepartmentDisciplineRepository _departmentDisciplineRepository;

        public DisciplineJobPositionDataSeeder(IDisciplineJobPositionRepository jobPositionRepository, IDepartmentDisciplineRepository disciplineRepository)
        {
            _disciplineJobPositionRepository = jobPositionRepository;
            _departmentDisciplineRepository = disciplineRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            var jobPositions = await _disciplineJobPositionRepository.GetListAsync();

            if (jobPositions.Count == 0)
            {
                var disciplines = await _departmentDisciplineRepository.GetListAsync();

                var entities = new List<DisciplineJobPosition>
                {
                    new() {
                        DepartmentDisciplineId = disciplines[0].Id,
                        Code = "DisciplineJobPosition001"
                    },
                    new() {
                        DepartmentDisciplineId = disciplines[1].Id,
                        Code = "DisciplineJobPosition002"
                    },
                    new() {
                        DepartmentDisciplineId = disciplines[2].Id,
                        Code = "JobPosition003"
                    },
                    new() {
                        DepartmentDisciplineId = disciplines[3].Id,
                        Code = "DisciplineJobPosition004"
                    },
                    new() {
                        DepartmentDisciplineId = disciplines[4].Id,
                        Code = "DisciplineJobPosition005"
                    }
                };

                await _disciplineJobPositionRepository.InsertManyAsync(entities, true);
            }
        }
    }
}
