using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace AvaPMIS.Main.Company
{
    public class Company : AuditedAggregateRoot<Guid>
    {
        public Nullable<Guid> ParentID { get; set; }
        public string Title { get; set; }
        public string RegisterationCode { get; set; }

        public List<Department.Department> Departments { get; set; }

    }
}
