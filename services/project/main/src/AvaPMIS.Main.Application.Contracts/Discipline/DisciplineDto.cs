using AvaPMIS.Main.Company;
using AvaPMIS.Main.Department;
using System;
using Volo.Abp.Application.Dtos;

namespace AvaPMIS.Main.Discipline
{
    public class DisciplineDto : ExtensibleEntityDto<Guid>
    {
        public Guid DepartmentId { get; set; }
        public string Code { get; set; }

        public DepartmentDto Department{ get; set; }
    }
}
