using AvaPMIS.Main.JobPosition;
using Nozhan.Abp.Utilities.Extensions.DataAnnotations;
using System;

namespace AvaPMIS.Main.Person
{
    public class CreateUpdatePersonDto
    {
        public Guid DisciplineJobPositionId { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string NationalCode { get; set; }

        [PersianMobileNumberValidator]
        public string Mobile { get; set; }
    }
}
