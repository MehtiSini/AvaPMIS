using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace AvaPMIS.Main.DefJobPosition
{
    public class DefJobPosition : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string Code { get; set; }

    }
}
