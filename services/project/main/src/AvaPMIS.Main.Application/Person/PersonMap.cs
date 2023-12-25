using AutoMapper;
using AvaPMIS.Main.Utility;

namespace AvaPMIS.Main.Person
{
    public class PersonMap : Profile
    {
        public PersonMap()
        {
            CreateMap<Person, PersonDto>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();

            CreateMap<Person, CreateUpdatePersonDto>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();

        }
    }
}
