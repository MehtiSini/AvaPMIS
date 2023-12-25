using System;

namespace AvaPMIS.Main.Discipline
{
    public class CreateUpdateDepartmentDisciplineDto
    { 
        public Guid CompanyDepartmentId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
