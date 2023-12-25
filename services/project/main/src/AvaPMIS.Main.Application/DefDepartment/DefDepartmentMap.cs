using AutoMapper;
using AvaPMIS.Main.Utility;

namespace AvaPMIS.Main.DefDepartment
{
    public class DefDepartmentMap : Profile
    {
        public DefDepartmentMap()
        {
            CreateMap<DefDepartment, DefDepartmentDto>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();

            CreateMap<DefDepartment, CreateUpdateDefDepartmentDto>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();

        }
    }
}