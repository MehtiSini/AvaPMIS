using System.Collections.Generic;
using System.Threading.Tasks;
using AvaPMIS.Main.DisciplineJobPosition;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace AvaPMIS.Main.Person
{
    public class PersonDataSeeder : IPersonDataSeeder, ITransientDependency
    {
        private readonly IPersonRepository _personRepository;
        private readonly IDisciplineJobPositionRepository _disciplinejobPositionRepository;

        public PersonDataSeeder(IPersonRepository personRepository, IDisciplineJobPositionRepository jobPositionRepository)
        {
            _personRepository = personRepository;
            _disciplinejobPositionRepository = jobPositionRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            var people = await _personRepository.GetListAsync();

            if (people.Count == 0)
            {
                var jobPositions = await _disciplinejobPositionRepository.GetListAsync();

                var entities = new List<Person>
                {
                    new() {
                        DisciplineJobPositionId = jobPositions[0].Id,
                        Name = "John",
                        Family = "Doe",
                        NationalCode = "1234567890",
                        Mobile = "09123456789"
                    },
                    new() {
                        DisciplineJobPositionId = jobPositions[1].Id,
                        Name = "Jane",
                        Family = "Doe",
                        NationalCode = "0987654321",
                        Mobile = "09098765432"
                    },
                    new() {
                        DisciplineJobPositionId = jobPositions[2].Id,
                        Name = "Alice",
                        Family = "Smith",
                        NationalCode = "9876543210",
                        Mobile = "09123456789"
                    },
                    new() {
                        DisciplineJobPositionId = jobPositions[3].Id,
                        Name = "Bob",
                        Family = "Johnson",
                        NationalCode = "0123456789",
                        Mobile = "09098765432"
                    },
                    new() {
                        DisciplineJobPositionId = jobPositions[4].Id,
                        Name = "Eva",
                        Family = "Williams",
                        NationalCode = "6789012345",
                        Mobile = "09123456789"
                    }
                };

                await _personRepository.InsertManyAsync(entities, true);
            }
        }
    }
}
