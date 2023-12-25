using AutoMapper;
using AvaPMIS.Main.DepartmentDiscipline;
using AvaPMIS.Main.Discipline;
using AvaPMIS.Main.Utility;

namespace AvaPMIS.Main.DepartmentDescipline
{
    public class DepartmentDisciplineMap : Profile
    {
        public DepartmentDisciplineMap()
        {
            CreateMap<DepartmentDiscipline.DepartmentDiscipline, DepartmentDisciplineDto>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();

            CreateMap<DepartmentDiscipline.DepartmentDiscipline, CreateUpdateDepartmentDisciplineDto>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();

        }
    }
}
