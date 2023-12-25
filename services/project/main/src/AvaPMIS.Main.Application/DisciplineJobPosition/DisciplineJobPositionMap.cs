using AutoMapper;
using AvaPMIS.Main.Utility;

namespace AvaPMIS.Main.DisciplineJobPosition
{
    public class DisciplineJobPositionMap : Profile
    {
        public DisciplineJobPositionMap()
        {
            CreateMap<DisciplineJobPosition, DisciplineJobPositionDto>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();

            CreateMap<DisciplineJobPosition, CreateUpdateDisciplineJobPositionDto>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();

        }
    }
}
