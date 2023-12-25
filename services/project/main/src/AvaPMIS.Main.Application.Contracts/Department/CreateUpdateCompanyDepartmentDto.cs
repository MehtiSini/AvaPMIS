using System;
using System.ComponentModel.DataAnnotations;

namespace AvaPMIS.Main.CompanyDepartment
{
    public class CreateUpdateCompanyDepartmentDto
    {
        public Guid? ParentID { get; set; }
        public Guid CompanyId{ get; set; }
        public string Code { get; set; }
    }
}
