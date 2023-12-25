using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace AvaPMIS.Main.DefDiscipline
{
    public class DefDiscipline : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string Code { get; set; }

    }
}
