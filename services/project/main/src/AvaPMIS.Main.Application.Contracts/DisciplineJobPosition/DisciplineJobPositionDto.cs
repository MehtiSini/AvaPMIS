using System;
using AvaPMIS.Main.DepartmentDiscipline;
using Volo.Abp.Application.Dtos;

namespace AvaPMIS.Main.DisciplineJobPosition
{
    public class DisciplineJobPositionDto : ExtensibleEntityDto<Guid>
    {
        public Guid JobPositionId { get; set; }
        public Guid DepartmentDisciplineId { get; set; }
        public DepartmentDisciplineDto DepartmentDiscipline   { get; set; }
    }   
}
