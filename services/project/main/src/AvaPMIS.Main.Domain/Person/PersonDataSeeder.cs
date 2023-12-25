using System.Collections.Generic;
using System.Threading.Tasks;
using AvaPMIS.Main.JobPosition;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace AvaPMIS.Main.Person
{
    public class PersonDataSeeder : IPersonDataSeeder, ITransientDependency
    {
        private readonly IPersonRepository _personRepository;
        private readonly IJobPositionRepository _jobPositionRepository;

        public PersonDataSeeder(IPersonRepository personRepository, IJobPositionRepository jobPositionRepository)
        {
            _personRepository = personRepository;
            _jobPositionRepository = jobPositionRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            var people = await _personRepository.GetListAsync();

            if (people.Count == 0)
            {
                var jobPositions = await _jobPositionRepository.GetListAsync();

                var entities = new List<Person>
                {
                    new() {
                        JobPositionId = jobPositions[0].Id,
                        Name = "John",
                        Family = "Doe",
                        NationalCode = "1234567890",
                        Mobile = "09123456789"
                    },
                    new() {
                        JobPositionId = jobPositions[1].Id,
                        Name = "Jane",
                        Family = "Doe",
                        NationalCode = "0987654321",
                        Mobile = "09098765432"
                    },
                    new() {
                        JobPositionId = jobPositions[2].Id,
                        Name = "Alice",
                        Family = "Smith",
                        NationalCode = "9876543210",
                        Mobile = "09123456789"
                    },
                    new() {
                        JobPositionId = jobPositions[3].Id,
                        Name = "Bob",
                        Family = "Johnson",
                        NationalCode = "0123456789",
                        Mobile = "09098765432"
                    },
                    new() {
                        JobPositionId = jobPositions[4].Id,
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
