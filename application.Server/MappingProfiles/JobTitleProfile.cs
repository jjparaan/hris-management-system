using application.API.Entities;
using application.API.Entities.DTOs;
using AutoMapper;

namespace application.API.MappingProfiles
{
    public class JobTitleProfile : Profile
    {
        public JobTitleProfile()
        {
            CreateMap<JobTitle, JobTitleDTO>().ReverseMap();
        }
    }
}
