using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace AvaPMIS.Main.Company
{
    public class CompanyDataSeeder : ICompanyDataSeeder, ITransientDependency
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyDataSeeder(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            var company = await _companyRepository.FirstOrDefaultAsync();

            if (company is null)
            {
                var companies = new List<Company>
                {
                    new() {
                        ParentID = null,
                        Title = "Company 1",
                        RegisterationCode = "ABC123"
                    },
                    new() {
                        ParentID = null,
                        Title = "Company 2",
                        RegisterationCode = "DEF456"
                    },
                    new() {
                        ParentID = null,
                        Title = "Company 3",
                        RegisterationCode = "GHI789"
                    },
                    new() {
                        ParentID = null,
                        Title = "Company 4",
                        RegisterationCode = "JKL012"
                    },
                    new() {
                        ParentID = null,
                        Title = "Company 5",
                        RegisterationCode = "MNO345"
                    }
                };

                await _companyRepository.InsertManyAsync(companies, true);
            }
        }
    }
}
