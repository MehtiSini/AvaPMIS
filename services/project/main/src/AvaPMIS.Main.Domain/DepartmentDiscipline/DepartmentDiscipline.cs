using System.Collections.Generic;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace AvaPMIS.Main.DepartmentDiscipline
{
    public class DepartmentDiscipline : AuditedAggregateRoot<Guid>
    {
        public Guid DefDicsiplineId { get; set; }
        public Guid CompanyDepartmentId { get; set; }
        public CompanyDepartment.CompanyDepartment CompanyDepartment { get; set; }
        public List<DisciplineJobPosition.DisciplineJobPosition> DiciplineJobPositions { get; set; }
        public DefDiscipline.DefDiscipline DefDiscipline { get; set; }
    }
}
