using System.Collections.Generic;
using System.Threading.Tasks;
using AvaPMIS.Main.CompanyDepartment;
using AvaPMIS.Main.Discipline;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace AvaPMIS.Main.DepartmentDiscipline
{
    public class DepartmentDisciplineDataSeeder : IDepartmentDisciplineDataSeeder, ITransientDependency
    {
        private readonly IDepartmentDisciplineRepository _departmentDisciplineRepository;
        private readonly ICompanyDepartmentRepository _companyDepartmentRepository;

        public DepartmentDisciplineDataSeeder(IDepartmentDisciplineRepository departmentDisciplineRepository, ICompanyDepartmentRepository companyDepartmentRepository)
        {
            _departmentDisciplineRepository = departmentDisciplineRepository;
            _companyDepartmentRepository = companyDepartmentRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            var departmentDisciplines = await _departmentDisciplineRepository.GetListAsync();

            if (departmentDisciplines.Count == 0)
            {
                var companyDepartments = await _companyDepartmentRepository.GetListAsync();

                var entities = new List<DepartmentDiscipline>
                {
                    new() {
                        CompanyDepartmentId = companyDepartments[0].Id,
                        Code = "DepartmentDiscipline001"
                    },
                    new() {
                        CompanyDepartmentId = companyDepartments[1].Id,
                        Code = "DepartmentDiscipline002"
                    },
                    new() {
                        CompanyDepartmentId = companyDepartments[2].Id,
                        Code = "DepartmentDiscipline003"
                    },
                    new() {
                        CompanyDepartmentId = companyDepartments[3].Id,
                        Code = "DepartmentDiscipline004"
                    },
                    new() {
                        CompanyDepartmentId = companyDepartments[4].Id,
                        Code = "DepartmentDiscipline005"
                    }
                };

                await _departmentDisciplineRepository.InsertManyAsync(entities, true);
            }
        }
    }
}
