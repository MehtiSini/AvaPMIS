using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace AvaPMIS.Main.DefJobPosition
{
    public class DefJobPosition : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public List<DisciplineJobPosition.DisciplineJobPosition> DisciplineJobPositions  { get; set; }

    }
}
