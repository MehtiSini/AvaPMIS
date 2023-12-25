using System.Threading.Tasks;
using AvaPMIS.Main.Company;
using AvaPMIS.Main.CompanyDepartment;
using AvaPMIS.Main.DefDepartment;
using AvaPMIS.Main.DefDiscipline;
using AvaPMIS.Main.DefJobPosition;
using AvaPMIS.Main.DepartmentDiscipline;
using AvaPMIS.Main.DisciplineJobPosition;
using AvaPMIS.Main.Person;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace AvaPMIS.Main
{
    public class MainDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
       
        private readonly ICompanyDataSeeder _companyDataSeeder;
        private readonly IDefDepartmentDataSeeder _defDepartmentDataSeeder;
        private readonly ICompanyDepartmentDataSeeder _companyDepartmentDataSeeder;
        private readonly IDefDisciplineDataSeeder _defDisciplineDataSeeder;
        private readonly IDepartmentDisciplineDataSeeder _depatrmentDisciplineDataSeeder ;
        private readonly IDefJobPositionDataSeeder _defJobPositionDataSeeder;
        private readonly IDisciplineJobPositionDataSeeder _disciplineJobPositionDataSeeder;
        private readonly IPersonDataSeeder _personDataSeeder;


        public MainDataSeederContributor(ICompanyDataSeeder companyDataSeeder, ICompanyDepartmentDataSeeder departmentDataSeeder, IDepartmentDisciplineDataSeeder departmentDisciplineDataSeeder, IDisciplineJobPositionDataSeeder disciplineJobPositionDataSeeder, IPersonDataSeeder personDataSeeder, IDefDepartmentDataSeeder defDepartmentDataSeeder, IDefDisciplineDataSeeder defDisciplineDataSeeder, IDefJobPositionDataSeeder defJobPositionDataSeeder)
        {
            _companyDataSeeder = companyDataSeeder;
            _companyDepartmentDataSeeder = departmentDataSeeder;
            _depatrmentDisciplineDataSeeder = departmentDisciplineDataSeeder;
            _disciplineJobPositionDataSeeder = disciplineJobPositionDataSeeder;
            _personDataSeeder = personDataSeeder;
            _defDepartmentDataSeeder = defDepartmentDataSeeder;
            _defDisciplineDataSeeder = defDisciplineDataSeeder;
            _defJobPositionDataSeeder = defJobPositionDataSeeder;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
          
            await _companyDataSeeder.SeedAsync(context);
            await _companyDepartmentDataSeeder.SeedAsync(context);
            await _depatrmentDisciplineDataSeeder.SeedAsync(context);
            await _disciplineJobPositionDataSeeder.SeedAsync(context);
            await _personDataSeeder.SeedAsync(context);

            ////
            await _defDepartmentDataSeeder.SeedAsync(context);
            await _defDisciplineDataSeeder.SeedAsync(context);
            await _defJobPositionDataSeeder.SeedAsync(context);

        }
    }
}
