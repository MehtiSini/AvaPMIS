using System.Collections.Generic;
using System.Threading.Tasks;
using AvaPMIS.Main.DepartmentDiscipline;
using AvaPMIS.Main.Discipline;
using AvaPMIS.Main.JobPosition;
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
                        Code = "JobPosition001"
                    },
                    new() {
                        DepartmentDisciplineId = disciplines[1].Id,
                        Code = "JobPosition002"
                    },
                    new() {
                        DepartmentDisciplineId = disciplines[2].Id,
                        Code = "JobPosition003"
                    },
                    new() {
                        DepartmentDisciplineId = disciplines[3].Id,
                        Code = "JobPosition004"
                    },
                    new() {
                        DepartmentDisciplineId = disciplines[4].Id,
                        Code = "JobPosition005"
                    }
                };

                await _disciplineJobPositionRepository.InsertManyAsync(entities, true);
            }
        }
    }
}
