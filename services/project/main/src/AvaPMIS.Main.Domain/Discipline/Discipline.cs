using System.Collections.Generic;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace AvaPMIS.Main.Discipline
{
    public class Discipline : AuditedAggregateRoot<Guid>
    {
        public Guid DepartmentId { get; set; }
        public string Code { get; set; }
        public Department.Department Department { get; set; }
        public List<JobPosition.JobPosition> JobPositions{ get; set; }

    }
}
