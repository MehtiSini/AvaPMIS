using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AvaPMIS.Main.Company;
using AvaPMIS.Main.CompanyDepartment;
using AvaPMIS.Main.DefDepartment;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace AvaPMIS.Main.CompanyDepartment
{
    public class CompanyDepartmentDataSeeder : ICompanyDepartmentDataSeeder, ITransientDependency
    {
        private readonly ICompanyDepartmentRepository _companyDepartmentRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IDefDepartmentRepository _defDepartmentRepository;

        public CompanyDepartmentDataSeeder(
            ICompanyDepartmentRepository companyDepartmentRepository,
            ICompanyRepository companyRepository,
            IDefDepartmentRepository defDepartmentRepository)
        {
            _companyDepartmentRepository = companyDepartmentRepository;
            _companyRepository = companyRepository;
            _defDepartmentRepository = defDepartmentRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            var companyDepartments = await _companyDepartmentRepository.GetListAsync();

            if (companyDepartments.Count == 0)
            {
                var companies = await _companyRepository.GetListAsync();
                var defDepartments = await _defDepartmentRepository.GetListAsync();

                var entities = new List<CompanyDepartment>
                {
                    new CompanyDepartment
                    {
                        DefDepartmentId = defDepartments[0].Id,
                        CompanyId = companies[0].Id,
                        ParentId = null,
                        DefDepartment = defDepartments[0]
                    },
                    new CompanyDepartment
                    {
                        DefDepartmentId = defDepartments[1].Id,
                        CompanyId = companies[1].Id,
                        DefDepartment = defDepartments[1]
                    },
                    new CompanyDepartment
                    {
                        DefDepartmentId = defDepartments[2].Id,
                        CompanyId = companies[2].Id,
                        DefDepartment = defDepartments[2]
                    },
                    new CompanyDepartment
                    {
                        DefDepartmentId = defDepartments[3].Id,
                        CompanyId = companies[3].Id,
                        DefDepartment = defDepartments[3]
                    },
                    new CompanyDepartment
                    {
                        DefDepartmentId = defDepartments[4].Id,
                        CompanyId = companies[4].Id,
                        DefDepartment = defDepartments[4]
                    }
                };

                await _companyDepartmentRepository.InsertManyAsync(entities, true);
            }
        }
    }
}
