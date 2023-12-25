using System;

namespace AvaPMIS.Main.DisciplineJobPosition
{
    public class CreateUpdateDisciplineJobPositionDto
    {
        public Guid DepartmentDisciplineId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

    }
}
