using System;
using AvaPMIS.Main.DepartmentDiscipline;
using Volo.Abp.Application.Dtos;

namespace AvaPMIS.Main.DisciplineJobPosition
{
    public class DisciplineJobPositionDto : ExtensibleEntityDto<Guid>
    {
        public Guid DepartmentDisciplineId { get; set; }
        public string Code { get; set; }

        public DepartmentDisciplineDto DepartmentDiscipline   { get; set; }
    }   
}
