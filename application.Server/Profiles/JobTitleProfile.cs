using application.Server.Entities;
using application.Server.Entities.DTOs;
using AutoMapper;

namespace application.Server.Profiles
{
    public class JobTitleProfile : Profile
    {
        public JobTitleProfile()
        {
            CreateMap<JobTitle, JobTitleDTO>().ReverseMap();
        }
    }
}
