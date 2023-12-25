using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace AvaPMIS.Main.Department
{
    public class Department : AuditedAggregateRoot<Guid>
    {
        public Nullable<Guid> ParentId { get; set; }
        public Guid CompanyId { get; set; }
        public string Code{ get; set; }
        public Company.Company Company{ get; set; }
        public List<Discipline.Discipline> Disciples { get; set; }
    }
}
