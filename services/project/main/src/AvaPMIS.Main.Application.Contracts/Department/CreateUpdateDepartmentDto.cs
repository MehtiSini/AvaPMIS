using System;
using System.ComponentModel.DataAnnotations;

namespace AvaPMIS.Main.Department
{
    public class CreateUpdateDepartmentDto
    {
        public Guid? ParentID { get; set; }
        public Guid CompanyId{ get; set; }
        public string Code { get; set; }
    }
}
