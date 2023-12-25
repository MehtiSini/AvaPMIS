using AutoMapper;
using AvaPMIS.Main.DefDepartment;
using AvaPMIS.Main.Utility;

namespace AvaPMIS.Main.DefDiscipline
{
    public class DefDisciplineMap : Profile
    {
        public DefDisciplineMap()
        {
            CreateMap<DefDiscipline, DefDisciplineDto>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();

            CreateMap<DefDiscipline, CreateUpdateDefDisciplineDto>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();

        }
    }
}
