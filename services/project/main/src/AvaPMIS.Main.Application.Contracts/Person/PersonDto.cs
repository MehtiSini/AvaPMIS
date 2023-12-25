using System;
using AvaPMIS.Main.JobPosition;
using Nozhan.Abp.Utilities.Extensions.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace AvaPMIS.Main.Person
{
    public class PersonDto : ExtensibleEntityDto<Guid>
    {
        public Guid JobPositionId { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string FullName { get { return Name + " " + Family; } }
        public string NationalCode { get; set; }

        [PersianMobileNumberValidator]
        public string Mobile { get; set; }
        public JobPositionDto JobPosition { get; set; }
    }
}
