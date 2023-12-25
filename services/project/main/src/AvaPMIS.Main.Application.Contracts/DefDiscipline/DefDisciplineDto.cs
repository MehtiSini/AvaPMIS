using System;
using Volo.Abp.Application.Dtos;

namespace AvaPMIS.Main.DefDiscipline
{
    public class DefDisciplineDto : ExtensibleEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Code { get; set; }

    }
}
