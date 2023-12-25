using AvaPMIS.Main.Company;
using AvaPMIS.Main.CompanyDepartment;
using System;
using Volo.Abp.Application.Dtos;

namespace AvaPMIS.Main.DepartmentDiscipline
{
    public class DepartmentDisciplineDto : ExtensibleEntityDto<Guid>
    {
        public Guid DisciplineId { get; set; }
        public Guid CompanyDepartmentId { get; set; }
        public CompanyDepartmentDto CompanyDepartment{ get; set; }
    }
}
