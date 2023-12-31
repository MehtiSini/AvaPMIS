﻿using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace AvaPMIS.Main.CompanyDepartment
{
    public class CompanyDepartment : AuditedAggregateRoot<Guid>
    {
        public Guid DefDepartmentId{ get; set; }
        public Guid CompanyId { get; set; }
        public Guid? ParentId { get; set; }
        public Company.Company Company{ get; set; }
        public List<DepartmentDiscipline.DepartmentDiscipline> DepartmentDisciplines { get; set; }
        public DefDepartment.DefDepartment DefDepartment { get; set; }

    }
}
