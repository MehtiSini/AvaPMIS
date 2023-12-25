using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace AvaPMIS.Main.DefDiscipline
{
    public class DefDiscipline : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public List<DepartmentDiscipline.DepartmentDiscipline> DepartmentDisciplines  { get; set; }

    }
}
