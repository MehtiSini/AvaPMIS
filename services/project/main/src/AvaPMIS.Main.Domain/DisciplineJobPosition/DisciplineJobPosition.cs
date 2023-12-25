using AvaPMIS.Main.Entities;
using System;
using System.Collections.Generic;

namespace AvaPMIS.Main.DisciplineJobPosition
{
    public class DisciplineJobPosition : AuditableAggregate<Guid>
    {
        public Guid DepartmentDisciplineId { get; set; }
        public string Code { get; set; }
        public DepartmentDiscipline.DepartmentDiscipline DepartmentDiscipline { get; set; }
        public List<Person.Person> People{ get; set; }

    }
}
