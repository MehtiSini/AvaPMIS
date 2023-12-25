using AutoMapper;
using AvaPMIS.Main.Utility;

namespace AvaPMIS.Main.Company
{
    public class CompanyMap : Profile
    {
        public CompanyMap()
        {
            CreateMap<Company, CompanyDto>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();

            CreateMap<Company, CreateUpdateCompanyDto>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();

        }
    }
}
