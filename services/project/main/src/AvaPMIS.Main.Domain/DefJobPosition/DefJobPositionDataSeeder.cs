using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace AvaPMIS.Main.DefJobPosition
{
    public class DefJobPositionDataSeeder : IDefJobPositionDataSeeder, ITransientDependency
    {
        private readonly IDefJobPositionRepository _defJobPositionRepository;

        public DefJobPositionDataSeeder(IDefJobPositionRepository defJobPositionRepository)
        {
            _defJobPositionRepository = defJobPositionRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            var defJobPositions = await _defJobPositionRepository.GetListAsync();

            if (defJobPositions.Count == 0)
            {
                var entities = new List<DefJobPosition>
                {
                    new DefJobPosition { Name = "Job Position 1", Code = "JP1" },
                    new DefJobPosition { Name = "Job Position 2", Code = "JP2" },
                    new DefJobPosition { Name = "Job Position 3", Code = "JP3" },
                    new DefJobPosition { Name = "Job Position 4", Code = "JP4" },
                    new DefJobPosition { Name = "Job Position 5", Code = "JP5" }
                };

                await _defJobPositionRepository.InsertManyAsync(entities, true);
            }
        }
    }
}
