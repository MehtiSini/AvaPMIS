using System.Threading.Tasks;
using AvaPMIS.Main.Company;
using AvaPMIS.Main.Department;
using AvaPMIS.Main.Discipline;
using AvaPMIS.Main.JobPosition;
using AvaPMIS.Main.Person;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace AvaPMIS.Main
{
    public class MainDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
       
        private readonly ICompanyDataSeeder _companyDataSeeder;
        private readonly IDepartmentDataSeeder _departmentDataSeeder;
        private readonly IDepartmentDisciplineDataSeeder _depatrmentDisciplineDataSeeder ;
        private readonly IDisciplineJobPositionDataSeeder _disciplineJobPositionDataSeeder;
        private readonly IPersonDataSeeder _personDataSeeder;


        public MainDataSeederContributor(ICompanyDataSeeder companyDataSeeder, IDepartmentDataSeeder departmentDataSeeder, IDepartmentDisciplineDataSeeder departmentDisciplineDataSeeder, IDisciplineJobPositionDataSeeder disciplineJobPositionDataSeeder, IPersonDataSeeder personDataSeeder)
        {
            _companyDataSeeder = companyDataSeeder;
            _departmentDataSeeder = departmentDataSeeder;
            _depatrmentDisciplineDataSeeder = departmentDisciplineDataSeeder;
            _disciplineJobPositionDataSeeder = disciplineJobPositionDataSeeder;
            _personDataSeeder = personDataSeeder;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
          
            await _companyDataSeeder.SeedAsync(context);
            await _departmentDataSeeder.SeedAsync(context);
            await _depatrmentDisciplineDataSeeder.SeedAsync(context);
            await _disciplineJobPositionDataSeeder.SeedAsync(context);
            await _personDataSeeder.SeedAsync(context);

        }
    }
}
