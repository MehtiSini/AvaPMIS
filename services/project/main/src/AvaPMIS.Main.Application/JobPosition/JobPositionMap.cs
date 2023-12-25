using AutoMapper;
using AvaPMIS.Main.Department;
using AvaPMIS.Main.Utility;

namespace AvaPMIS.Main.JobPosition
{
    public class JobPositionMap : Profile
    {
        public JobPositionMap()
        {
            CreateMap<JobPosition, JobPositionDto>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();

            CreateMap<JobPosition, CreateUpdateJobPositionDto>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();

        }
    }
}
