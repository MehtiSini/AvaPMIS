using AutoMapper;
using AvaPMIS.Main.Utility;

namespace AvaPMIS.Main.DefJobPosition
{
    public class DefJobPositionMap : Profile
    {
        public DefJobPositionMap()
        {
            CreateMap<DefJobPosition,DefJobPositionDto>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();

            CreateMap<DefJobPosition, CreateUpdateJobPositionDto>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();

        }
    }
}
