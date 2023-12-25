using System;
using AvaPMIS.Main.Company;
using Volo.Abp.Application.Dtos;

namespace AvaPMIS.Main.Department
{
    public class DepartmentDto : ExtensibleEntityDto<Guid>
    {
        public Guid CompanyId{ get; set; }
        public Nullable<Guid> ParentId  { get; set; }
        public string Code { get; set; }

        public CompanyDto Company{ get; set; }

    }
}
