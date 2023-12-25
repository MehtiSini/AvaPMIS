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
        private readonly IDisciplineDataSeeder _disciplineDataSeeder ;
        private readonly IJobPositionDataSeeder _jobPositionDataSeeder;
        private readonly IPersonDataSeeder _personDataSeeder;


        public MainDataSeederContributor(ICompanyDataSeeder companyDataSeeder, IDepartmentDataSeeder departmentDataSeeder, IDisciplineDataSeeder disciplineDataSeeder, IJobPositionDataSeeder jobPositionDataSeeder, IPersonDataSeeder personDataSeeder)
        {
            _companyDataSeeder = companyDataSeeder;
            _departmentDataSeeder = departmentDataSeeder;
            _disciplineDataSeeder = disciplineDataSeeder;
            _jobPositionDataSeeder = jobPositionDataSeeder;
            _personDataSeeder = personDataSeeder;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
          
            await _companyDataSeeder.SeedAsync(context);
            await _departmentDataSeeder.SeedAsync(context);
            await _disciplineDataSeeder.SeedAsync(context);
            await _jobPositionDataSeeder.SeedAsync(context);
            await _personDataSeeder.SeedAsync(context);

        }
    }
}
