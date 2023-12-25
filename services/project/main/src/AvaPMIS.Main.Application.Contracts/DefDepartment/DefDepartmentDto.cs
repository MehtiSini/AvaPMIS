using System;
using Volo.Abp.Application.Dtos;

namespace AvaPMIS.Main.DefDepartment
{
    public class DefDepartmentDto : ExtensibleEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Code { get; set; }

    }
}
