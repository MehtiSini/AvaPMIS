using System.Collections.Generic;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace AvaPMIS.Main.DefDepartment
{
    public  class DefDepartment : AuditedAggregateRoot<Guid>
    {
        public string Name{ get; set; }
        public string Code { get; set; }

    }
}
