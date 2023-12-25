using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AvaPMIS.Main.DefJobPosition;
using AvaPMIS.Main.DepartmentDiscipline;
using AvaPMIS.Main.DisciplineJobPosition;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace AvaPMIS.Main.DisciplineJobPosition
{
    public class DisciplineJobPositionDataSeeder : IDisciplineJobPositionDataSeeder, ITransientDependency
    {
        private readonly IDisciplineJobPositionRepository _disciplineJobPositionRepository;
        private readonly IDepartmentDisciplineRepository _departmentDisciplineRepository;
        private readonly IDefJobPositionRepository _defJobPositionRepository;

        public DisciplineJobPositionDataSeeder(
            IDisciplineJobPositionRepository disciplineJobPositionRepository,
            IDepartmentDisciplineRepository departmentDisciplineRepository,
            IDefJobPositionRepository defJobPositionRepository)
        {
            _disciplineJobPositionRepository = disciplineJobPositionRepository;
            _departmentDisciplineRepository = departmentDisciplineRepository;
            _defJobPositionRepository = defJobPositionRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            var disciplineJobPositions = await _disciplineJobPositionRepository.GetListAsync();

            if (disciplineJobPositions.Count == 0)
            {
                var departmentDisciplines = await _departmentDisciplineRepository.GetListAsync();
                var defJobPositions = await _defJobPositionRepository.GetListAsync();

                var entities = new List<DisciplineJobPosition>
                {
                    new DisciplineJobPosition
                    {
                        DefJobPositionId = defJobPositions[0].Id,
                        DepartmentDisciplineId = departmentDisciplines[0].Id,
                        DefJobPosition = defJobPositions[0]
                    },
                    new DisciplineJobPosition
                    {
                        DefJobPositionId = defJobPositions[1].Id,
                        DepartmentDisciplineId = departmentDisciplines[1].Id,
                        DefJobPosition = defJobPositions[1]
                    },
                    new DisciplineJobPosition
                    {
                        DefJobPositionId = defJobPositions[2].Id,
                        DepartmentDisciplineId = departmentDisciplines[2].Id,
                        DefJobPosition = defJobPositions[2]
                    },
                    new DisciplineJobPosition
                    {
                        DefJobPositionId = defJobPositions[3].Id,
                        DepartmentDisciplineId = departmentDisciplines[3].Id,
                        DefJobPosition = defJobPositions[3]
                    },
                    new DisciplineJobPosition
                    {
                        DefJobPositionId = defJobPositions[4].Id,
                        DepartmentDisciplineId = departmentDisciplines[4].Id,
                        DefJobPosition = defJobPositions[4]
                    }
                };

                await _disciplineJobPositionRepository.InsertManyAsync(entities, true);
            }
        }
    }
}
