using AutoMapper;
using FullStackSession6.Model;
using TaskEight.DTOs;

namespace TaskEight.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Tasks, TasksDTO>();
            CreateMap<Tasks, TaskSummaryDTO>();
        }
    }
}
