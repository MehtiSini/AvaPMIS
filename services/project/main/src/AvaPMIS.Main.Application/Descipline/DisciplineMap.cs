using AutoMapper;
using AvaPMIS.Main.Department;
using AvaPMIS.Main.Discipline;
using AvaPMIS.Main.Utility;

namespace AvaPMIS.Main.Descipline
{
    public class DisciplineMap : Profile
    {
        public DisciplineMap()
        {
            CreateMap<Discipline.Discipline, DisciplineDto>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();

            CreateMap<Discipline.Discipline, CreateUpdateDisciplineDto>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();

        }
    }
}
