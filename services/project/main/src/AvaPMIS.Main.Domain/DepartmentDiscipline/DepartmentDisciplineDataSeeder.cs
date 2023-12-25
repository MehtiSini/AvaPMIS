using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AvaPMIS.Main.CompanyDepartment;
using AvaPMIS.Main.DefDiscipline;
using AvaPMIS.Main.DepartmentDiscipline;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace AvaPMIS.Main.DepartmentDiscipline
{
    public class DepartmentDisciplineDataSeeder : IDepartmentDisciplineDataSeeder, ITransientDependency
    {
        private readonly IDepartmentDisciplineRepository _departmentDisciplineRepository;
        private readonly ICompanyDepartmentRepository _companyDepartmentRepository;
        private readonly IDefDisciplineRepository _defDisciplineRepository;

        public DepartmentDisciplineDataSeeder(
            IDepartmentDisciplineRepository departmentDisciplineRepository,
            ICompanyDepartmentRepository companyDepartmentRepository,
            IDefDisciplineRepository defDisciplineRepository)
        {
            _departmentDisciplineRepository = departmentDisciplineRepository;
            _companyDepartmentRepository = companyDepartmentRepository;
            _defDisciplineRepository = defDisciplineRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            var departmentDisciplines = await _departmentDisciplineRepository.GetListAsync();

            if (departmentDisciplines.Count == 0)
            {
                var companyDepartments = await _companyDepartmentRepository.GetListAsync();
                var defDisciplines = await _defDisciplineRepository.GetListAsync();

                var entities = new List<DepartmentDiscipline>
                {
                    new DepartmentDiscipline
                    {
                        DefDicsiplineId = defDisciplines[0].Id,
                        CompanyDepartmentId = companyDepartments[0].Id,
                        DefDiscipline = defDisciplines[0]
                    },
                    new DepartmentDiscipline
                    {
                        DefDicsiplineId = defDisciplines[1].Id,
                        CompanyDepartmentId = companyDepartments[1].Id,
                        DefDiscipline = defDisciplines[1]
                    },
                    new DepartmentDiscipline
                    {
                        DefDicsiplineId = defDisciplines[2].Id,
                        CompanyDepartmentId = companyDepartments[2].Id,
                        DefDiscipline = defDisciplines[2]
                    },
                    new DepartmentDiscipline
                    {
                        DefDicsiplineId = defDisciplines[3].Id,
                        CompanyDepartmentId = companyDepartments[3].Id,
                        DefDiscipline = defDisciplines[3]
                    },
                    new DepartmentDiscipline
                    {
                        DefDicsiplineId = defDisciplines[4].Id,
                        CompanyDepartmentId = companyDepartments[4].Id,
                        DefDiscipline = defDisciplines[4]
                    }
                };

                await _departmentDisciplineRepository.InsertManyAsync(entities, true);
            }
        }
    }
}
