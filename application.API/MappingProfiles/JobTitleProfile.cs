using application.ApplicationLayer.DTOs;
using application.Domain.Entities;
using AutoMapper;

namespace application.Domain.MappingProfiles
{
    public class JobTitleProfile : Profile
    {
        public JobTitleProfile()
        {
            CreateMap<JobTitle, JobTitleDTO>().ReverseMap();
        }
    }
}
