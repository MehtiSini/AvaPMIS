using System;
using AvaPMIS.Main.Company;
using Volo.Abp.Application.Dtos;

namespace AvaPMIS.Main.CompanyDepartment
{
    public class CompanyDepartmentDto : ExtensibleEntityDto<Guid>
    {
        public Guid CompanyId{ get; set; }
        public Nullable<Guid> ParentId  { get; set; }
        public string Code { get; set; }

        public CompanyDto Company{ get; set; }

    }
}
