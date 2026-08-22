using AutoMapper;
using FullStackSession6.Model;
using TaskEight.DTOs;

namespace TaskEight.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            // Tasks => TasksDTO
            CreateMap<Tasks, TasksDTO>()
            .ForMember(
                dest => dest.UserName, 
                opt => opt.MapFrom(src => src.User != null ? src.User.Name : null)
            );

            // Tasks => TaskSummaryDTO
            CreateMap<Tasks, TaskSummaryDTO>();

            // CreateTaskRequest => Tasks
            CreateMap<CreateTaskRequest, Tasks>()
            .ForMember(
                dest => dest.Id,
                opt => opt.Ignore()
            )
            .ForMember(
                dest => dest.IsCompleted, 
                opt => opt.MapFrom(_ => false)
            )
            .ForMember(
                dest => dest.CreatedAt, 
                opt => opt.MapFrom(_ => DateTime.UtcNow)
            )
            .ForMember(
                dest => dest.UpdatedAt, 
                opt => opt.Ignore()
            )
            .ForMember(
                dest => dest.User, 
                opt => opt.Ignore()
            );

            // UpdateTaskRequest => Tasks
            CreateMap<UpdateTaskRequest, Tasks>()
            .ForMember(
                dest => dest.Id, 
                opt => opt.Ignore()
            )
            .ForMember(
                dest => dest.CreatedAt, 
                opt => opt.Ignore()
            )
            .ForMember(
                dest => dest.UpdatedAt, 
                opt => opt.Ignore()
            )
            .ForMember(
                dest => dest.User, 
                opt => opt.Ignore()
            );
        }
    }
}
