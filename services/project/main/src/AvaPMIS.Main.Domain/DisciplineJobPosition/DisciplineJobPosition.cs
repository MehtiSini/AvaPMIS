using AvaPMIS.Main.DefJobPosition;
using AvaPMIS.Main.Entities;
using System;
using System.Collections.Generic;

namespace AvaPMIS.Main.DisciplineJobPosition
{
    public class DisciplineJobPosition : AuditableAggregate<Guid>
    {
        public Guid DefJobPositionId { get; set; }
        public Guid DepartmentDisciplineId { get; set; }
        public DepartmentDiscipline.DepartmentDiscipline DepartmentDiscipline { get; set; }
        public List<Person.Person> People{ get; set; }
        public DefJobPosition.DefJobPosition DefJobPosition { get; set; }

    }
}
