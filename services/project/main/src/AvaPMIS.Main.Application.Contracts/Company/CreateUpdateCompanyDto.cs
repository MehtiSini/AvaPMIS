using System;
using System.ComponentModel.DataAnnotations;

namespace AvaPMIS.Main.Company
{
    public class CreateUpdateCompanyDto
    {
        public Nullable<Guid> ParentID { get; set; }
        public string Title { get; set; }
        public string RegisterationCode { get; set; }

    }
}
