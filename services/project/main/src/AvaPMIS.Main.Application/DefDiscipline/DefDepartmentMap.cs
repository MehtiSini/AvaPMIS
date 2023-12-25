using AutoMapper;
using AvaPMIS.Main.DefDepartment;
using AvaPMIS.Main.Utility;

namespace AvaPMIS.Main.DefDiscipline
{
    public class DefDepartmentMap : Profile
    {
        public DefDepartmentMap()
        {
            CreateMap<DefDepartment.DefDepartment, DefDepartmentDto>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();

            CreateMap<DefDepartment.DefDepartment, CreateUpdateDefDepartmentDto>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();

        }
    }
}