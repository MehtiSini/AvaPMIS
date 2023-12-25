using System;
using Volo.Abp.Application.Dtos;

namespace AvaPMIS.Main.Company
{
    public class CompanyDto : ExtensibleEntityDto<Guid>
    {
        public Nullable<Guid> ParentId { get; set; }
        public string Title { get; set; }
        public string RegisterationCode { get; set; }

    }
}
