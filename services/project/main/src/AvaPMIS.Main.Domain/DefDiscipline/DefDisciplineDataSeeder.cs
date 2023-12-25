using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AvaPMIS.Main.DefDiscipline;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace AvaPMIS.Main.DefDiscipline
{
    public class DefDisciplineDataSeeder : IDefDisciplineDataSeeder, ITransientDependency
    {
        private readonly IDefDisciplineRepository _defDisciplineRepository;

        public DefDisciplineDataSeeder(IDefDisciplineRepository defDisciplineRepository)
        {
            _defDisciplineRepository = defDisciplineRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            var defDisciplines = await _defDisciplineRepository.GetListAsync();

            if (defDisciplines.Count == 0)
            {
                var entities = new List<DefDiscipline>
                {
                    new DefDiscipline { Name = "Discipline 1", Code = "DC1" },
                    new DefDiscipline { Name = "Discipline 2", Code = "DC2" },
                    new DefDiscipline { Name = "Discipline 3", Code = "DC3" },
                    new DefDiscipline { Name = "Discipline 4", Code = "DC4" },
                    new DefDiscipline { Name = "Discipline 5", Code = "DC5" }
                };

                await _defDisciplineRepository.InsertManyAsync(entities, true);
            }
        }
    }
}
