using System.Collections.Generic;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace AvaPMIS.Main.DepartmentDiscipline
{
    public class DepartmentDiscipline : AuditedAggregateRoot<Guid>
    {
        public Guid DepartmentId { get; set; }
        public string Code { get; set; }
        public Department.Department Department { get; set; }
        public List<DisciplineJobPosition.DisciplineJobPosition> DiciplineJobPositions{ get; set; }

    }
}
