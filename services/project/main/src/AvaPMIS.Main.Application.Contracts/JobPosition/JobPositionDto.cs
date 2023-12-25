using System;
using AvaPMIS.Main.Discipline;
using Volo.Abp.Application.Dtos;

namespace AvaPMIS.Main.JobPosition
{
    public class JobPositionDto : ExtensibleEntityDto<Guid>
    {
        public Guid DisciplineId { get; set; }
        public string Code { get; set; }
        //public int DisciplineType { get; set; }

        public DisciplineDto Discipline   { get; set; }
    }   
}
