using System.Collections.Generic;
using System.Threading.Tasks;
using AvaPMIS.Main.Company;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace AvaPMIS.Main.Department
{
    public class DepartmentDataSeeder : IDepartmentDataSeeder, ITransientDependency
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ICompanyRepository _companyRepository;

        public DepartmentDataSeeder(IDepartmentRepository departmentRepository, ICompanyRepository companyRepository)
        {
            _departmentRepository = departmentRepository;
            _companyRepository = companyRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            var departments = await _departmentRepository.GetListAsync();

            if (departments.Count == 0)
            {
                var companies = await _companyRepository.GetListAsync();

                var entities = new List<Department>
                {
                    new() {
                        ParentId = null,
                        CompanyId = companies[0].Id,
                        Code = "Dept001"
                    },
                    new() {
                        ParentId = null,
                        CompanyId = companies[1].Id,
                        Code = "Dept002"
                    },
                    new() {
                        ParentId = null,
                        CompanyId = companies[2].Id,
                        Code = "Dept003"
                    },
                    new() {
                        ParentId = null,
                        CompanyId = companies[3].Id,
                        Code = "Dept004"
                    },
                    new() {
                        ParentId = null,
                        CompanyId = companies[4].Id,
                        Code = "Dept005"
                    }
                };

                await _departmentRepository.InsertManyAsync(entities, true);
            }
        }
    }
}
