using System;
using AvaPMIS.Main.Company;
using Volo.Abp.Application.Dtos;

namespace AvaPMIS.Main.CompanyDepartment
{
    public class CompanyDepartmentDto : ExtensibleEntityDto<Guid>
    {
        public Guid DepartmentId { get; set; }
        public Guid CompanyId{ get; set; }
        public Guid? ParentId  { get; set; }
        public CompanyDto Company{ get; set; }

    }
}
