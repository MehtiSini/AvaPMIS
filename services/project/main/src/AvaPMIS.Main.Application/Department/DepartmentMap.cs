using AutoMapper;
using AvaPMIS.Main.Utility;

namespace AvaPMIS.Main.Department
{
    public class DepartmentMap : Profile
    {
        public DepartmentMap()
        {
            CreateMap<Department, DepartmentDto>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();

            CreateMap<Department, CreateUpdateDepartmentDto>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();

        }
    }
}
