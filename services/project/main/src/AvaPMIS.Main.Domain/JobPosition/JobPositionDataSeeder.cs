using System.Collections.Generic;
using System.Threading.Tasks;
using AvaPMIS.Main.Discipline;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace AvaPMIS.Main.JobPosition
{
    public class JobPositionDataSeeder : IJobPositionDataSeeder, ITransientDependency
    {
        private readonly IJobPositionRepository _jobPositionRepository;
        private readonly IDisciplineRepository _disciplineRepository;

        public JobPositionDataSeeder(IJobPositionRepository jobPositionRepository, IDisciplineRepository disciplineRepository)
        {
            _jobPositionRepository = jobPositionRepository;
            _disciplineRepository = disciplineRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            var jobPositions = await _jobPositionRepository.GetListAsync();

            if (jobPositions.Count == 0)
            {
                var disciplines = await _disciplineRepository.GetListAsync();

                var entities = new List<JobPosition>
                {
                    new() {
                        DisciplineId = disciplines[0].Id,
                        Code = "JobPosition001"
                    },
                    new() {
                        DisciplineId = disciplines[1].Id,
                        Code = "JobPosition002"
                    },
                    new() {
                        DisciplineId = disciplines[2].Id,
                        Code = "JobPosition003"
                    },
                    new() {
                        DisciplineId = disciplines[3].Id,
                        Code = "JobPosition004"
                    },
                    new() {
                        DisciplineId = disciplines[4].Id,
                        Code = "JobPosition005"
                    }
                };

                await _jobPositionRepository.InsertManyAsync(entities, true);
            }
        }
    }
}
