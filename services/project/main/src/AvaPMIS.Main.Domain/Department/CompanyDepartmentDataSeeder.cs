using System.Collections.Generic;
using System.Threading.Tasks;
using AvaPMIS.Main.Company;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace AvaPMIS.Main.CompanyDepartment
{
    public class CompanyDepartmentDataSeeder : ICompanyDepartmentDataSeeder, ITransientDependency
    {
        private readonly ICompanyDepartmentRepository _companyDepartmentRepository;
        private readonly ICompanyRepository _companyRepository;

        public CompanyDepartmentDataSeeder(ICompanyDepartmentRepository companyDepartmentRepository, ICompanyRepository companyRepository)
        {
            _companyDepartmentRepository = companyDepartmentRepository;
            _companyRepository = companyRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            var departments = await _companyDepartmentRepository.GetListAsync();

            if (departments.Count == 0)
            {
                var companies = await _companyRepository.GetListAsync();

                var entities = new List<CompanyDepartment>
                {
                    new() {
                        ParentId = null,
                        CompanyId = companies[0].Id,
                        Code = "CompanyDept001"
                    },
                    new() {
                        ParentId = null,
                        CompanyId = companies[1].Id,
                        Code = "CompanyDept002"
                    },
                    new() {
                        ParentId = null,
                        CompanyId = companies[2].Id,
                        Code = "CompanyDept003"
                    },
                    new() {
                        ParentId = null,
                        CompanyId = companies[3].Id,
                        Code = "CompanyDept004"
                    },
                    new() {
                        ParentId = null,
                        CompanyId = companies[4].Id,
                        Code = "CompanyDept005"
                    }
                };

                await _companyDepartmentRepository.InsertManyAsync(entities, true);
            }
        }
    }
}
