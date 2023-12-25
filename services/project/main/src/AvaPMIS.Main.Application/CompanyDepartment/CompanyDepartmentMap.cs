using AutoMapper;
using AvaPMIS.Main.Utility;

namespace AvaPMIS.Main.CompanyDepartment
{
    public class CompanyDepartmentMap : Profile
    {
        public CompanyDepartmentMap()
        {
            CreateMap<CompanyDepartment, CompanyDepartmentDto>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();

            CreateMap<CompanyDepartment, CreateUpdateCompanyDepartmentDto>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();

        }
    }
}
