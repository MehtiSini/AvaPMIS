using System;
using AvaPMIS.Main.Entities;
using Nozhan.Abp.Utilities.Extensions.DataAnnotations;

namespace AvaPMIS.Main.Person
{
    public class Person : AuditableAggregate<Guid>
    {
        public Guid DisciplineJobPositionId{ get; set; }
        public string Name { get; set; }

        public string Family { get; set; }

        public string FullName { get { return Name + " " + Family; } }

        public string NationalCode { get; set; }

        [PersianMobileNumberValidator]
        public string Mobile { get; set; }

        public DisciplineJobPosition.DisciplineJobPosition DisciplineJobPosition{ get; set; }
    }
}
