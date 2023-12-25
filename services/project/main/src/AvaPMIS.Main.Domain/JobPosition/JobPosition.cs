using AvaPMIS.Main.Entities;
using System;
using System.Collections.Generic;

namespace AvaPMIS.Main.JobPosition
{
    public class JobPosition : AuditableAggregate<Guid>
    {
        public Guid DisciplineId { get; set; }
        public string Code { get; set; }
        public Discipline.Discipline Discipline { get; set; }
        public List<Person.Person> People{ get; set; }

    }
}
